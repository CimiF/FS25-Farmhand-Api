using System.Text.Json;
using FS25Gateway.Infrastructure.Telemetry.Frames;
using FS25Gateway.Infrastructure.Telemetry.Handlers;
using Microsoft.Extensions.Logging;

namespace FS25Gateway.Infrastructure.Transport;

/// <summary>Routes Farmhand JSON frames to handlers based on their event name.</summary>
public sealed class FarmhandFrameRouter(
	IWorkersFrameHandler workersHandler,
	IMissionsFrameHandler missionsHandler,
	IFieldsFrameHandler fieldsHandler,
	IStorageFrameHandler storageHandler,
	ISitesFrameHandler sitesHandler,
	IWeatherFrameHandler weatherHandler,
	ISettingsFrameHandler settingsHandler,
	IPlayersFrameHandler playersHandler,
	ILogger<FarmhandFrameRouter> logger) : IFarmhandFrameReceiver
{
	private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
	{
		PropertyNameCaseInsensitive = true
	};

	/// <inheritdoc />
	public async Task ReceiveAsync(JsonDocument frame, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(frame);
		cancellationToken.ThrowIfCancellationRequested();

		if (!frame.RootElement.TryGetProperty("event", out var eventProperty) ||
			eventProperty.ValueKind != JsonValueKind.String)
		{
			logger.LogWarning("Ignoring Farmhand frame without a string event property.");
			return;
		}

		var eventName = eventProperty.GetString();
		if (string.IsNullOrWhiteSpace(eventName))
		{
			logger.LogWarning("Ignoring Farmhand frame with an empty event property.");
			return;
		}

		switch (eventName)
		{
			case "workers":
				await DispatchAsync<WorkersFrame>(frame, workersHandler.HandleAsync, cancellationToken).ConfigureAwait(false);
				break;
			case "missions":
				await DispatchAsync<MissionsFrame>(frame, missionsHandler.HandleAsync, cancellationToken).ConfigureAwait(false);
				break;
			case "fields":
				await DispatchAsync<FieldsFrame>(frame, fieldsHandler.HandleAsync, cancellationToken).ConfigureAwait(false);
				break;
			case "storage":
				await DispatchAsync<StorageFrame>(frame, storageHandler.HandleAsync, cancellationToken).ConfigureAwait(false);
				break;
			case "sites":
				await DispatchAsync<SitesFrame>(frame, sitesHandler.HandleAsync, cancellationToken).ConfigureAwait(false);
				break;
			case "weather":
				await DispatchAsync<WeatherFrame>(frame, weatherHandler.HandleAsync, cancellationToken).ConfigureAwait(false);
				break;
			case "settings":
				await DispatchAsync<SettingsFrame>(frame, settingsHandler.HandleAsync, cancellationToken).ConfigureAwait(false);
				break;
			case "players":
				await DispatchAsync<PlayersFrame>(frame, playersHandler.HandleAsync, cancellationToken).ConfigureAwait(false);
				break;
			default:
				logger.LogDebug("Ignoring unknown Farmhand event {EventName}.", eventName);
				break;
		}
	}

	private async Task DispatchAsync<TFrame>(
		JsonDocument frame,
		Func<TFrame, CancellationToken, Task> handler,
		CancellationToken cancellationToken)
		where TFrame : class
	{
		TFrame? typedFrame;
		try
		{
			typedFrame = frame.RootElement.Deserialize<TFrame>(SerializerOptions);
		}
		catch (JsonException exception)
		{
			logger.LogWarning(exception, "Ignoring invalid Farmhand {FrameType} frame.", typeof(TFrame).Name);
			logger.LogDebug("Invalid Farmhand {FrameType} frame content: {FrameContent}", typeof(TFrame).Name, frame.RootElement.ToString());
			return;
		}

		if (typedFrame is null)
		{
			logger.LogWarning("Ignoring empty Farmhand {FrameType} frame.", typeof(TFrame).Name);
			return;
		}

		await handler(typedFrame, cancellationToken).ConfigureAwait(false);
	}
}
