using System.Text.Json;

namespace FS25Gateway.Infrastructure.Transport;

/// <summary>Receives one parsed Farmhand telemetry frame.</summary>
public interface IFarmhandFrameReceiver
{
	/// <summary>Receives a parsed JSON frame.</summary>
	/// <param name="frame">The parsed Farmhand frame.</param>
	/// <param name="cancellationToken">The operation cancellation token.</param>
	Task ReceiveAsync(JsonDocument frame, CancellationToken cancellationToken);
}
