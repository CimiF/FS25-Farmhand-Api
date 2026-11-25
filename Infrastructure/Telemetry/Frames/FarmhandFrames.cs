using System.Text.Json.Serialization;

namespace FS25Gateway.Infrastructure.Telemetry.Frames;

/// <summary>Contains the worker telemetry frame.</summary>
public sealed record WorkersFrame
{
	/// <summary>Gets the workers in the frame.</summary>
	public IList<WorkerFrameItem> Items { get; init; } = new List<WorkerFrameItem>();
}

/// <summary>Contains one worker telemetry item.</summary>
public sealed class WorkerFrameItem
{
	public long Id { get; init; }
	public string? Name { get; init; }
	public string? Kind { get; init; }
	public string? JobType { get; init; }
	public string? Job { get; init; }
	public double? X { get; init; }
	public double? Z { get; init; }
	public int? Field { get; init; }
	public string? State { get; init; }
	public bool Blocked { get; init; }
	public double? StillMs { get; init; }
	public double? Fuel { get; init; }
	public double? Damage { get; init; }
	public string? StopReason { get; init; }
	public IList<FillFrameItem> Fills { get; init; } = new List<FillFrameItem>();
	public IList<ImplementFrameItem> Implements { get; init; } = new List<ImplementFrameItem>();
}

/// <summary>Contains a fill level from a Farmhand frame.</summary>
public sealed class FillFrameItem
{
	[JsonPropertyName("type")]
	public string? FillType { get; init; }

	public double Level { get; init; }
	public double Lvl { get; init; }
	public double Cap { get; init; }
	public double Capacity { get; init; }
}

/// <summary>Contains an implement from a Farmhand frame.</summary>
public sealed class ImplementFrameItem
{
	public string? Name { get; init; }
	public double? Damage { get; init; }
	public IList<FillFrameItem> Fills { get; init; } = new List<FillFrameItem>();
}

/// <summary>Contains the field telemetry frame.</summary>
public sealed record FieldsFrame
{
	public int Owned { get; init; }
	public int Total { get; init; }
	public int Period { get; init; }
	public int DayInPeriod { get; init; }
	public int DaysPerPeriod { get; init; }
	public IList<FieldFrameItem> Rows { get; init; } = new List<FieldFrameItem>();
}

/// <summary>Contains one field telemetry item.</summary>
public sealed class FieldFrameItem
{
	public int N { get; init; }
	public int? Farmland { get; init; }
	public double? Ha { get; init; }
	public double? X { get; init; }
	public double? Z { get; init; }
	public string? Crop { get; init; }
	public string? State { get; init; }
	public int? Growth { get; init; }
	public int? GrowthMax { get; init; }
	public bool Closing { get; init; }
	public IList<string> Needs { get; init; } = new List<string>();
	public SoilFrameItem? Soil { get; init; }
}

/// <summary>Contains raw soil telemetry.</summary>
public sealed class SoilFrameItem
{
	public int? GrowthState { get; init; }
	public int? LastGrowthState { get; init; }
	public int? Lime { get; init; }
	public int? Plow { get; init; }
	public int? Roller { get; init; }
	public int? Spray { get; init; }
	public int? SprayType { get; init; }
	public int? Stones { get; init; }
	public int? StubbleShred { get; init; }
	public int? Weed { get; init; }
	public double? WeedFactor { get; init; }
	public double? Water { get; init; }
	public int? Ground { get; init; }
}

/// <summary>Contains the sites telemetry frame.</summary>
public sealed record SitesFrame
{
	public double? TimeAdjustment { get; init; }
	public IList<AnimalPenFrameItem> Pens { get; init; } = new List<AnimalPenFrameItem>();
	public IList<ProductionSiteFrameItem> Productions { get; init; } = new List<ProductionSiteFrameItem>();
}

/// <summary>Contains one animal pen telemetry item.</summary>
public sealed class AnimalPenFrameItem
{
	public long Id { get; init; }
	public string? Name { get; init; }
	public string? Type { get; init; }
	public int Count { get; init; }
	public int Max { get; init; }
	public double? Productivity { get; init; }
	public double? Health { get; init; }
	public FoodFrameItem? Food { get; init; }
	public WaterFrameItem? Water { get; init; }
	public IList<AnimalOutputFrameItem> Outputs { get; init; } = new List<AnimalOutputFrameItem>();
}

/// <summary>Contains animal food storage telemetry.</summary>
public sealed class FoodFrameItem
{
	public double Lvl { get; init; }
	public double Cap { get; init; }
	public double? PerHour { get; init; }
	public IList<FoodGroupFrameItem> Groups { get; init; } = new List<FoodGroupFrameItem>();
}

/// <summary>Contains one animal food group.</summary>
public sealed class FoodGroupFrameItem
{
	public string? Name { get; init; }
	public double? Weight { get; init; }
	public double Lvl { get; init; }
	public IList<string> Fills { get; init; } = new List<string>();
}

/// <summary>Contains animal water storage telemetry.</summary>
public sealed class WaterFrameItem
{
	public double Lvl { get; init; }
	public double Cap { get; init; }
	public double? PerHour { get; init; }
	public bool Auto { get; init; }
}

/// <summary>Contains an animal output telemetry item.</summary>
public sealed class AnimalOutputFrameItem
{
	public string? Fill { get; init; }
	public string? Kind { get; init; }
	public double Lvl { get; init; }
	public double Cap { get; init; }
	public double? PerHour { get; init; }
}

/// <summary>Contains one production site telemetry item.</summary>
public sealed class ProductionSiteFrameItem
{
	public long Id { get; init; }
	public string? Name { get; init; }
	public bool On { get; init; }
	public bool? PalletFull { get; init; }
	public string? Stopped { get; init; }
	public IList<ProductionLineFrameItem> Lines { get; init; } = new List<ProductionLineFrameItem>();
	public IList<InputFrameItem> Inputs { get; init; } = new List<InputFrameItem>();
	public IList<OutputFrameItem> Outputs { get; init; } = new List<OutputFrameItem>();
}

/// <summary>Contains one production line telemetry item.</summary>
public sealed class ProductionLineFrameItem
{
	public string? Name { get; init; }
	public bool On { get; init; }
	public string? Status { get; init; }
	public double? CyclesPerHour { get; init; }
}

/// <summary>Contains a production input telemetry item.</summary>
public sealed class InputFrameItem
{
	public string? Fill { get; init; }
	public double Lvl { get; init; }
	public double Cap { get; init; }
	public double? PerHour { get; init; }
}

/// <summary>Contains a production output telemetry item.</summary>
public sealed class OutputFrameItem
{
	public string? Fill { get; init; }
	public string? Mode { get; init; }
	public double Lvl { get; init; }
	public double Cap { get; init; }
	public double? PerHour { get; init; }
}

/// <summary>Contains the storage telemetry frame.</summary>
public sealed record StorageFrame
{
	public IList<StorageFrameItem> Items { get; init; } = new List<StorageFrameItem>();
	public IList<InventoryTotalFrameItem> Totals { get; init; } = new List<InventoryTotalFrameItem>();
	public IList<PalletTotalFrameItem> Pallets { get; init; } = new List<PalletTotalFrameItem>();
}

/// <summary>Contains one storage telemetry item.</summary>
public sealed class StorageFrameItem
{
	public long Id { get; init; }
	public string? Name { get; init; }
	public double? Capacity { get; init; }
	public IList<FillFrameItem> Fills { get; init; } = new List<FillFrameItem>();
}

/// <summary>Contains one inventory total.</summary>
public sealed class InventoryTotalFrameItem
{
	public string? Fill { get; init; }
	public double Lvl { get; init; }
}

/// <summary>Contains one pallet inventory total.</summary>
public sealed class PalletTotalFrameItem
{
	public string? Fill { get; init; }
	public double Lvl { get; init; }
	public int Count { get; init; }
}

/// <summary>Contains the missions telemetry frame.</summary>
public sealed record MissionsFrame
{
	public ContractLimitFrameItem? Limit { get; init; }
	public bool CanManage { get; init; }
	public IList<MissionFrameItem> Items { get; init; } = new List<MissionFrameItem>();
}

/// <summary>Contains contract capacity information.</summary>
public sealed class ContractLimitFrameItem
{
	public int Active { get; init; }
	public int Max { get; init; }
}

/// <summary>Contains one mission telemetry item.</summary>
public sealed class MissionFrameItem
{
	public string? Id { get; init; }
	public string? Type { get; init; }
	public string? Title { get; init; }
	public string? Desc { get; init; }
	public string? Status { get; init; }
	public bool Mine { get; init; }
	public decimal Reward { get; init; }
	public double? Completion { get; init; }
	public int? Field { get; init; }
	public double? Ha { get; init; }
	public string? Fill { get; init; }
	public double? X { get; init; }
	public double? Z { get; init; }
	public int? EndDay { get; init; }
	public int? EndDayTime { get; init; }
}

/// <summary>Contains the weather telemetry frame.</summary>
public sealed record WeatherFrame
{
	public int? Now { get; init; }
	public double? Temp { get; init; }
	public double? Min { get; init; }
	public double? Max { get; init; }
	public bool Raining { get; init; }
	public int? RainInMin { get; init; }
	public IList<ForecastFrameItem> Items { get; init; } = new List<ForecastFrameItem>();
}

/// <summary>Contains one forecast telemetry item.</summary>
public sealed class ForecastFrameItem
{
	public string? Type { get; init; }
	public int Day { get; init; }
	public int Hour { get; init; }
	public int Minute { get; init; }
	public int DurationMin { get; init; }
}

/// <summary>Contains the settings telemetry frame.</summary>
public sealed record SettingsFrame
{
	public NeedsFrameItem? Needs { get; init; }
	public HelperFrameItem? Helper { get; init; }
	public int? DaysPerPeriod { get; init; }
	public int? GrowthMode { get; init; }
	public int? FuelUsage { get; init; }
	public int? EconomicDifficulty { get; init; }
	public bool? Snow { get; init; }
	public IDictionary<string, int>? Max { get; init; }
	public IDictionary<string, CropSettingsFrameItem>? Crops { get; init; }
}

/// <summary>Contains enabled field needs from the settings frame.</summary>
public sealed class NeedsFrameItem
{
	public bool Lime { get; init; }
	public bool Plow { get; init; }
	public bool Weeds { get; init; }
	public bool Stones { get; init; }
}

/// <summary>Contains helper refill settings from the settings frame.</summary>
public sealed class HelperFrameItem
{
	public bool Fuel { get; init; }
	public bool Seeds { get; init; }
	public bool Fertilizer { get; init; }
	public int? ManureSource { get; init; }
	public int? SlurrySource { get; init; }
}

/// <summary>Contains settings for one crop.</summary>
public sealed class CropSettingsFrameItem
{
	public IList<bool> Plant { get; init; } = new List<bool>();
	public IList<bool> Harvest { get; init; } = new List<bool>();
	public bool NeedsRolling { get; init; }
	public bool GrowthRequiresLime { get; init; }
	public bool PlantsWeed { get; init; }
}

/// <summary>Contains the players telemetry frame.</summary>
public sealed record PlayersFrame
{
	public bool Multiplayer { get; init; }
	public bool Server { get; init; }
	public bool Master { get; init; }
	public long? Me { get; init; }
	public IList<PlayerFrameItem> Users { get; init; } = new List<PlayerFrameItem>();
}

/// <summary>Contains one player telemetry item.</summary>
public sealed class PlayerFrameItem
{
	public long Id { get; init; }
	public string? Name { get; init; }
	public string? Farm { get; init; }
	public bool Master { get; init; }
}
