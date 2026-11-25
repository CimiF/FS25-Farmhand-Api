namespace FS25Gateway.Domain.Models;

/// <summary>Represents the current weather and forecast.</summary>
public sealed class WeatherState
{
	/// <summary>Gets the current weather type.</summary>
	public string? CurrentType { get; init; }

	/// <summary>Gets the current temperature in Celsius.</summary>
	public double? Temperature { get; init; }

	/// <summary>Gets today's minimum temperature.</summary>
	public double? MinimumTemperature { get; init; }

	/// <summary>Gets today's maximum temperature.</summary>
	public double? MaximumTemperature { get; init; }

	/// <summary>Gets whether it is currently raining.</summary>
	public bool IsRaining { get; init; }

	/// <summary>Gets the minutes until rain, when known.</summary>
	public int? RainInMinutes { get; init; }

	/// <summary>Gets the forecast items.</summary>
	public IList<ForecastItem> Forecast { get; init; } = new List<ForecastItem>();
}

/// <summary>Represents one forecast period.</summary>
public sealed class ForecastItem
{
	/// <summary>Gets the weather type.</summary>
	public string? WeatherType { get; init; }

	/// <summary>Gets the game day.</summary>
	public int Day { get; init; }

	/// <summary>Gets the start hour.</summary>
	public int Hour { get; init; }

	/// <summary>Gets the start minute.</summary>
	public int Minute { get; init; }

	/// <summary>Gets the duration in game minutes.</summary>
	public int DurationMinutes { get; init; }
}

/// <summary>Represents save-level game settings relevant to the farm.</summary>
public sealed class GameSettings
{
	/// <summary>Gets whether lime is required.</summary>
	public bool LimeRequired { get; init; }

	/// <summary>Gets whether plowing is required.</summary>
	public bool PlowingRequired { get; init; }

	/// <summary>Gets whether weeds are enabled.</summary>
	public bool WeedsEnabled { get; init; }

	/// <summary>Gets whether stones are enabled.</summary>
	public bool StonesEnabled { get; init; }

	/// <summary>Gets the number of days per period.</summary>
	public int? DaysPerPeriod { get; init; }

	/// <summary>Gets the growth mode.</summary>
	public int? GrowthMode { get; init; }

	/// <summary>Gets the fuel usage setting.</summary>
	public int? FuelUsage { get; init; }

	/// <summary>Gets the economic difficulty.</summary>
	public int? EconomicDifficulty { get; init; }

	/// <summary>Gets whether snow is enabled.</summary>
	public bool SnowEnabled { get; init; }

	/// <summary>Gets the maximum soil values by category.</summary>
	public IDictionary<string, int> MaximumSoilValues { get; init; } = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

	/// <summary>Gets crop-specific settings by crop name.</summary>
	public IDictionary<string, CropSettings> Crops { get; init; } = new Dictionary<string, CropSettings>(StringComparer.OrdinalIgnoreCase);
}

/// <summary>Represents growth settings for a crop.</summary>
public sealed class CropSettings
{
	/// <summary>Gets the planting periods.</summary>
	public IList<bool> PlantingPeriods { get; init; } = new List<bool>();

	/// <summary>Gets the harvest periods.</summary>
	public IList<bool> HarvestPeriods { get; init; } = new List<bool>();

	/// <summary>Gets whether rolling is required.</summary>
	public bool NeedsRolling { get; init; }

	/// <summary>Gets whether growth requires lime.</summary>
	public bool GrowthRequiresLime { get; init; }

	/// <summary>Gets whether the crop plants weeds.</summary>
	public bool PlantsWeeds { get; init; }
}

/// <summary>Represents the multiplayer players and permissions.</summary>
public sealed class PlayerState
{
	/// <summary>Gets whether the save is multiplayer.</summary>
	public bool IsMultiplayer { get; init; }

	/// <summary>Gets whether the current player is the server.</summary>
	public bool IsServer { get; init; }

	/// <summary>Gets whether the current player is a master user.</summary>
	public bool IsMaster { get; init; }

	/// <summary>Gets the current player identifier.</summary>
	public long? CurrentPlayerId { get; init; }

	/// <summary>Gets the connected users.</summary>
	public IList<Player> Users { get; init; } = new List<Player>();
}

/// <summary>Represents one connected player.</summary>
public sealed class Player
{
	/// <summary>Gets the player identifier.</summary>
	public long Id { get; init; }

	/// <summary>Gets the player name.</summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>Gets the farm name or identifier.</summary>
	public string? Farm { get; init; }

	/// <summary>Gets whether the player is a master user.</summary>
	public bool IsMaster { get; init; }
}

/// <summary>Represents aggregate inventory totals.</summary>
public sealed class InventoryTotals
{
	/// <summary>Gets total stored levels keyed by fill type.</summary>
	public IDictionary<string, double> Fills { get; init; } = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

	/// <summary>Gets pallet and bale totals keyed by fill type.</summary>
	public IDictionary<string, double> Pallets { get; init; } = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
}

/// <summary>Represents the active alerts for the farm.</summary>
public sealed class AlertsState
{
	/// <summary>Gets the active alerts.</summary>
	public IList<Alert> Items { get; init; } = new List<Alert>();
}

/// <summary>Represents one farm alert.</summary>
public sealed class Alert
{
	/// <summary>Gets the alert identifier.</summary>
	public Guid Id { get; init; }

	/// <summary>Gets the alert severity.</summary>
	public Domain.Enums.AlertSeverity Severity { get; init; }

	/// <summary>Gets the alert code.</summary>
	public string? Code { get; init; }

	/// <summary>Gets the alert message.</summary>
	public string Message { get; init; } = string.Empty;

	/// <summary>Gets the alert creation time.</summary>
	public DateTime CreatedAt { get; init; }

	/// <summary>Gets whether the alert has been acknowledged.</summary>
	public bool IsAcknowledged { get; init; }
}
