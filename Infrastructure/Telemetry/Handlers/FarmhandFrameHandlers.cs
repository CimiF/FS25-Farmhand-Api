using FS25Gateway.Infrastructure.Telemetry.Frames;

namespace FS25Gateway.Infrastructure.Telemetry.Handlers;

/// <summary>Handles workers telemetry frames.</summary>
public interface IWorkersFrameHandler
{
	/// <summary>Handles a workers frame.</summary>
	Task HandleAsync(WorkersFrame frame, CancellationToken cancellationToken);
}

/// <summary>Handles missions telemetry frames.</summary>
public interface IMissionsFrameHandler
{
	/// <summary>Handles a missions frame.</summary>
	Task HandleAsync(MissionsFrame frame, CancellationToken cancellationToken);
}

/// <summary>Handles fields telemetry frames.</summary>
public interface IFieldsFrameHandler
{
	/// <summary>Handles a fields frame.</summary>
	Task HandleAsync(FieldsFrame frame, CancellationToken cancellationToken);
}

/// <summary>Handles storage telemetry frames.</summary>
public interface IStorageFrameHandler
{
	/// <summary>Handles a storage frame.</summary>
	Task HandleAsync(StorageFrame frame, CancellationToken cancellationToken);
}

/// <summary>Handles sites telemetry frames.</summary>
public interface ISitesFrameHandler
{
	/// <summary>Handles a sites frame.</summary>
	Task HandleAsync(SitesFrame frame, CancellationToken cancellationToken);
}

/// <summary>Handles weather telemetry frames.</summary>
public interface IWeatherFrameHandler
{
	/// <summary>Handles a weather frame.</summary>
	Task HandleAsync(WeatherFrame frame, CancellationToken cancellationToken);
}

/// <summary>Handles settings telemetry frames.</summary>
public interface ISettingsFrameHandler
{
	/// <summary>Handles a settings frame.</summary>
	Task HandleAsync(SettingsFrame frame, CancellationToken cancellationToken);
}

/// <summary>Handles players telemetry frames.</summary>
public interface IPlayersFrameHandler
{
	/// <summary>Handles a players frame.</summary>
	Task HandleAsync(PlayersFrame frame, CancellationToken cancellationToken);
}

/// <summary>Provides no-op frame handling until domain mapping is enabled.</summary>
public sealed class NoOpFarmhandFrameHandlers :
	IWorkersFrameHandler,
	IMissionsFrameHandler,
	IFieldsFrameHandler,
	IStorageFrameHandler,
	ISitesFrameHandler,
	IWeatherFrameHandler,
	ISettingsFrameHandler,
	IPlayersFrameHandler
{
	/// <inheritdoc />
	public Task HandleAsync(WorkersFrame frame, CancellationToken cancellationToken) => CompleteAsync(frame, cancellationToken);

	/// <inheritdoc />
	public Task HandleAsync(MissionsFrame frame, CancellationToken cancellationToken) => CompleteAsync(frame, cancellationToken);

	/// <inheritdoc />
	public Task HandleAsync(FieldsFrame frame, CancellationToken cancellationToken) => CompleteAsync(frame, cancellationToken);

	/// <inheritdoc />
	public Task HandleAsync(StorageFrame frame, CancellationToken cancellationToken) => CompleteAsync(frame, cancellationToken);

	/// <inheritdoc />
	public Task HandleAsync(SitesFrame frame, CancellationToken cancellationToken) => CompleteAsync(frame, cancellationToken);

	/// <inheritdoc />
	public Task HandleAsync(WeatherFrame frame, CancellationToken cancellationToken) => CompleteAsync(frame, cancellationToken);

	/// <inheritdoc />
	public Task HandleAsync(SettingsFrame frame, CancellationToken cancellationToken) => CompleteAsync(frame, cancellationToken);

	/// <inheritdoc />
	public Task HandleAsync(PlayersFrame frame, CancellationToken cancellationToken) => CompleteAsync(frame, cancellationToken);

	private static Task CompleteAsync<TFrame>(TFrame frame, CancellationToken cancellationToken)
		where TFrame : notnull
	{
		ArgumentNullException.ThrowIfNull(frame);
		cancellationToken.ThrowIfCancellationRequested();
		return Task.CompletedTask;
	}
}
