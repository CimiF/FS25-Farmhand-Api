using System.Text.Json;

namespace FS25Gateway.Infrastructure.Transport;

/// <summary>Discards frames until a frame-processing receiver is implemented.</summary>
public sealed class NoOpFarmhandFrameReceiver : IFarmhandFrameReceiver
{
	/// <inheritdoc />
	public Task ReceiveAsync(JsonDocument frame, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(frame);
		cancellationToken.ThrowIfCancellationRequested();
		return Task.CompletedTask;
	}
}
