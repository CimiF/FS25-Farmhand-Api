using FS25Gateway.Domain.Enums;

namespace FS25Gateway.Domain.Models;

/// <summary>Describes the current game-world clock and calendar.</summary>
public sealed class WorldInfo
{
	/// <summary>Gets the current game day.</summary>
	public int Day { get; init; }

	/// <summary>Gets the current in-game hour.</summary>
	public int Hour { get; init; }

	/// <summary>Gets the current in-game minute.</summary>
	public int Minute { get; init; }

	/// <summary>Gets the current in-game time in milliseconds since midnight.</summary>
	public long DayTime { get; init; }

	/// <summary>Gets the current season or period name.</summary>
	public string? Season { get; init; }

	/// <summary>Gets the current calendar period.</summary>
	public int? Period { get; init; }
}

/// <summary>Represents a vehicle owned or operated by the farm.</summary>
public sealed class Vehicle
{
	/// <summary>Gets the vehicle identifier.</summary>
	public long Id { get; init; }

	/// <summary>Gets the display name.</summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>Gets the current position.</summary>
	public VehiclePosition? Position { get; set; }

	/// <summary>Gets the worker state, when a worker is assigned.</summary>
	public WorkerState? Worker { get; set; }

	/// <summary>Gets the implements attached to the vehicle.</summary>
	public IList<Implement> Implements { get; init; } = new List<Implement>();

	/// <summary>Gets the fill levels reported by the vehicle.</summary>
	public IList<FillLevel> Fills { get; init; } = new List<FillLevel>();

	/// <summary>Gets the fuel percentage.</summary>
	public double? FuelPercent { get; set; }

	/// <summary>Gets the damage ratio between zero and one.</summary>
	public double? Damage { get; set; }
}

/// <summary>Represents the worker operating a vehicle.</summary>
public sealed class WorkerState
{
	/// <summary>Gets the worker status.</summary>
	public WorkerStatus Status { get; init; }

	/// <summary>Gets the worker controller kind.</summary>
	public VehicleWorkerKind Kind { get; init; }

	/// <summary>Gets the job type.</summary>
	public string? JobType { get; init; }

	/// <summary>Gets the human-readable job description.</summary>
	public string? Job { get; init; }

	/// <summary>Gets the field currently associated with the job.</summary>
	public int? FieldId { get; init; }

	/// <summary>Gets the time for which the vehicle has been stationary.</summary>
	public TimeSpan? StillFor { get; init; }

	/// <summary>Gets the reason why the worker stopped.</summary>
	public string? StopReason { get; init; }
}

/// <summary>Represents a vehicle position in the game world.</summary>
public sealed class VehiclePosition
{
	/// <summary>Gets the world X coordinate.</summary>
	public double X { get; init; }

	/// <summary>Gets the world Z coordinate.</summary>
	public double Z { get; init; }

	/// <summary>Gets the associated field number, when known.</summary>
	public int? FieldId { get; init; }
}

/// <summary>Represents an implement attached to a vehicle.</summary>
public sealed class Implement
{
	/// <summary>Gets the implement name.</summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>Gets the fill levels reported by the implement.</summary>
	public IList<FillLevel> Fills { get; init; } = new List<FillLevel>();

	/// <summary>Gets the damage ratio between zero and one.</summary>
	public double? Damage { get; init; }

	/// <summary>Gets whether the implement is currently attached.</summary>
	public bool IsAttached { get; init; } = true;
}

/// <summary>Represents the amount of one fill type in a container.</summary>
public sealed class FillLevel
{
	/// <summary>Gets the fill type name.</summary>
	public string FillType { get; init; } = string.Empty;

	/// <summary>Gets the current level.</summary>
	public double Level { get; init; }

	/// <summary>Gets the maximum capacity.</summary>
	public double Capacity { get; init; }
}
