using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FS25Gateway.Infrastructure.Transport;

/// <summary>Reads newline-delimited JSON frames from the Farmhand widget named pipe.</summary>
public sealed class FarmhandPipeReader(
	IFarmhandFrameReceiver frameReceiver,
	ILogger<FarmhandPipeReader> logger) : BackgroundService
{
	private const string PipeName = "flowkits_fs25_edge";
	private const int ConnectTimeoutMilliseconds = 5_000;
	private static readonly TimeSpan ReconnectDelay = TimeSpan.FromSeconds(5);

	/// <inheritdoc />
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			try
			{
				await ReadConnectionAsync(stoppingToken).ConfigureAwait(false);
			}
			catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
			{
				break;
			}
			catch (TimeoutException exception)
			{
				logger.LogWarning(exception, "Farmhand pipe {PipeName} was not available within the connection timeout.", PipeName);
			}
			catch (IOException exception)
			{
				logger.LogWarning(exception, "Farmhand pipe {PipeName} disconnected or could not be opened.", PipeName);
			}
			finally
			{
				if (!stoppingToken.IsCancellationRequested)
				{
					logger.LogInformation("Retrying Farmhand pipe {PipeName} connection in {DelaySeconds} seconds.", PipeName, ReconnectDelay.TotalSeconds);
					await Task.Delay(ReconnectDelay, stoppingToken).ConfigureAwait(false);
				}
			}
		}

		logger.LogInformation("Farmhand pipe reader stopped.");
	}

	private async Task ReadConnectionAsync(CancellationToken cancellationToken)
	{
		logger.LogInformation("Attempting to connect to Farmhand pipe {PipeName}.", PipeName);

		await using var pipe = new NamedPipeServerStream(
					"flowkits_fs25_edge",
					PipeDirection.In,
					1,
					PipeTransmissionMode.Byte,
					PipeOptions.Asynchronous);

		await pipe.WaitForConnectionAsync(cancellationToken).ConfigureAwait(false);
		logger.LogInformation("Connected to Farmhand pipe {PipeName}.", PipeName);

		using var reader = new StreamReader(
			pipe,
			new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true),
			detectEncodingFromByteOrderMarks: false,
			bufferSize: 4096,
			leaveOpen: false);

		while (!cancellationToken.IsCancellationRequested)
		{
			var line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);
			if (line is null)
			{
				logger.LogWarning("Farmhand pipe {PipeName} disconnected.", PipeName);
				return;
			}

			if (string.IsNullOrWhiteSpace(line))
			{
				continue;
			}

			try
			{
				using var frame = JsonDocument.Parse(line);
				await frameReceiver.ReceiveAsync(frame, cancellationToken).ConfigureAwait(false);
			}
			catch (JsonException exception)
			{
				logger.LogWarning(exception, "Ignoring an invalid JSON frame received from Farmhand pipe {PipeName}.", PipeName);
			}
		}
	}
}
