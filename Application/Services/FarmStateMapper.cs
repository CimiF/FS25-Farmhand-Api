using FS25Gateway.Application.Services.Mappers;
using FS25Gateway.Infrastructure.Telemetry.Frames;

namespace FS25Gateway.Application.Services;

/// <summary>Coordinates incremental mapping of Farmhand frames into the farm aggregate.</summary>
public sealed class FarmStateMapper(
	WorkersFrameHandler workers,
	FieldsFrameMapper fields,
	SitesFrameMapper sites,
	StorageFrameMapper storage,
	MissionsFrameMapper missions,
	WeatherFrameMapper weather,
	SettingsFrameMapper settings,
	PlayersFrameMapper players)
{
	/// <summary>Maps a workers frame.</summary>
	public void Map(WorkersFrame frame) => workers.Map(frame);

	/// <summary>Maps a fields frame.</summary>
	public void Map(FieldsFrame frame) => fields.Map(frame);

	/// <summary>Maps a sites frame.</summary>
	public void Map(SitesFrame frame) => sites.Map(frame);

	/// <summary>Maps a storage frame.</summary>
	public void Map(StorageFrame frame) => storage.Map(frame);

	/// <summary>Maps a missions frame.</summary>
	public void Map(MissionsFrame frame) => missions.Map(frame);

	/// <summary>Maps a weather frame.</summary>
	public void Map(WeatherFrame frame) => weather.Map(frame);

	/// <summary>Maps a settings frame.</summary>
	public void Map(SettingsFrame frame) => settings.Map(frame);

	/// <summary>Maps a players frame.</summary>
	public void Map(PlayersFrame frame) => players.Map(frame);
}
