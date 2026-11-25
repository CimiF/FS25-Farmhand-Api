using FS25Gateway.Domain.Enums;

namespace FS25Gateway.Domain.Models;

/// <summary>Represents one farm field.</summary>
public sealed class Field
{
	/// <summary>Gets the field number.</summary>
	public int Id { get; init; }

	/// <summary>Gets the farmland number.</summary>
	public int? FarmlandId { get; init; }

	/// <summary>Gets the field area in hectares.</summary>
	public double? Hectares { get; init; }

	/// <summary>Gets the field position.</summary>
	public VehiclePosition? Position { get; init; }

	/// <summary>Gets the current crop, or null when fallow.</summary>
	public string? Crop { get; init; }

	/// <summary>Gets the field lifecycle state.</summary>
	public FieldState State { get; init; }

	/// <summary>Gets the current growth level.</summary>
	public int? Growth { get; init; }

	/// <summary>Gets the maximum growth level.</summary>
	public int? GrowthMaximum { get; init; }

	/// <summary>Gets whether the harvest window is closing.</summary>
	public bool Closing { get; init; }

	/// <summary>Gets the field soil state.</summary>
	public SoilState Soil { get; init; } = new();

	/// <summary>Gets the current field needs.</summary>
	public IList<string> Needs { get; init; } = new List<string>();
}

/// <summary>Represents the raw soil state of a field.</summary>
public sealed class SoilState
{
	/// <summary>Gets the current growth state.</summary>
	public int? GrowthState { get; init; }

	/// <summary>Gets the previous growth state.</summary>
	public int? LastGrowthState { get; init; }

	/// <summary>Gets the lime state.</summary>
	public int? Lime { get; init; }

	/// <summary>Gets the plowing state.</summary>
	public int? Plow { get; init; }

	/// <summary>Gets the rolling state.</summary>
	public int? Roller { get; init; }

	/// <summary>Gets the spray state.</summary>
	public int? Spray { get; init; }

	/// <summary>Gets the spray type.</summary>
	public int? SprayType { get; init; }

	/// <summary>Gets the stones state.</summary>
	public int? Stones { get; init; }

	/// <summary>Gets the stubble shredding state.</summary>
	public int? StubbleShred { get; init; }

	/// <summary>Gets the weed state.</summary>
	public int? Weed { get; init; }

	/// <summary>Gets the weed factor.</summary>
	public double? WeedFactor { get; init; }

	/// <summary>Gets the water level.</summary>
	public double? Water { get; init; }

	/// <summary>Gets the ground type.</summary>
	public int? Ground { get; init; }
}

/// <summary>Represents a contract known to the farm.</summary>
public sealed class Contract
{
	/// <summary>Gets the unique contract identifier.</summary>
	public string Id { get; init; } = string.Empty;

	/// <summary>Gets the contract type.</summary>
	public string? Type { get; init; }

	/// <summary>Gets the contract title.</summary>
	public string? Title { get; init; }

	/// <summary>Gets the contract description.</summary>
	public string? Description { get; init; }

	/// <summary>Gets the contract status.</summary>
	public ContractStatus Status { get; init; }

	/// <summary>Gets whether the contract belongs to the farm.</summary>
	public bool IsOwned { get; init; }

	/// <summary>Gets the reward.</summary>
	public decimal Reward { get; init; }

	/// <summary>Gets the contract position.</summary>
	public VehiclePosition? Position { get; init; }

	/// <summary>Gets the associated field number.</summary>
	public int? FieldId { get; init; }

	/// <summary>Gets the completion ratio.</summary>
	public double? Completion { get; init; }

	/// <summary>Gets the deadline in game time.</summary>
	public DateTime? Deadline { get; init; }
}
