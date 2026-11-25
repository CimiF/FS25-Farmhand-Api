using FS25Gateway.Domain.Enums;

namespace FS25Gateway.Domain.Models;

/// <summary>Represents a production site owned by the farm.</summary>
public sealed class ProductionSite
{
	/// <summary>Gets the network identifier.</summary>
	public long Id { get; init; }

	/// <summary>Gets the display name.</summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>Gets whether at least one production line is enabled.</summary>
	public bool IsEnabled { get; init; }

	/// <summary>Gets the production lines.</summary>
	public IList<ProductionLine> Lines { get; init; } = new List<ProductionLine>();

	/// <summary>Gets the input buffers.</summary>
	public IList<InputBuffer> Inputs { get; init; } = new List<InputBuffer>();

	/// <summary>Gets the output buffers.</summary>
	public IList<OutputBuffer> Outputs { get; init; } = new List<OutputBuffer>();

	/// <summary>Gets the reason the site is stopped, when applicable.</summary>
	public ProductionStopReason? StopReason { get; init; }
}

/// <summary>Represents one production line.</summary>
public sealed class ProductionLine
{
	/// <summary>Gets the line name.</summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>Gets whether the line is enabled.</summary>
	public bool IsEnabled { get; init; }

	/// <summary>Gets the line status.</summary>
	public string? Status { get; init; }

	/// <summary>Gets the number of cycles per game hour.</summary>
	public double? CyclesPerHour { get; init; }
}

/// <summary>Represents a production input buffer.</summary>
public sealed class InputBuffer
{
	/// <summary>Gets the fill type.</summary>
	public string FillType { get; init; } = string.Empty;

	/// <summary>Gets the current level.</summary>
	public double Level { get; init; }

	/// <summary>Gets the capacity.</summary>
	public double Capacity { get; init; }

	/// <summary>Gets the consumption rate per game hour.</summary>
	public double? PerHour { get; init; }
}

/// <summary>Represents a production output buffer.</summary>
public sealed class OutputBuffer
{
	/// <summary>Gets the fill type.</summary>
	public string FillType { get; init; } = string.Empty;

	/// <summary>Gets the output mode.</summary>
	public string? Mode { get; init; }

	/// <summary>Gets the current level.</summary>
	public double Level { get; init; }

	/// <summary>Gets the capacity.</summary>
	public double Capacity { get; init; }

	/// <summary>Gets the production rate per game hour.</summary>
	public double? PerHour { get; init; }
}

/// <summary>Represents an animal pen owned by the farm.</summary>
public sealed class AnimalPen
{
	/// <summary>Gets the network identifier.</summary>
	public long Id { get; init; }

	/// <summary>Gets the display name.</summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>Gets the animal type.</summary>
	public string? AnimalType { get; init; }

	/// <summary>Gets the current animal count.</summary>
	public int Count { get; init; }

	/// <summary>Gets the maximum animal count.</summary>
	public int Maximum { get; init; }

	/// <summary>Gets the productivity ratio.</summary>
	public double? Productivity { get; init; }

	/// <summary>Gets the average health.</summary>
	public double? Health { get; init; }

	/// <summary>Gets the animal outputs.</summary>
	public IList<AnimalOutput> Outputs { get; init; } = new List<AnimalOutput>();

	/// <summary>Gets the food storage.</summary>
	public FoodStorage? Food { get; init; }

	/// <summary>Gets the water storage.</summary>
	public WaterStorage? Water { get; init; }
}

/// <summary>Represents an output produced by an animal pen.</summary>
public sealed class AnimalOutput
{
	/// <summary>Gets the fill type.</summary>
	public string FillType { get; init; } = string.Empty;

	/// <summary>Gets the output kind.</summary>
	public string? Kind { get; init; }

	/// <summary>Gets the current level.</summary>
	public double Level { get; init; }

	/// <summary>Gets the capacity.</summary>
	public double Capacity { get; init; }

	/// <summary>Gets the production rate per game hour.</summary>
	public double? PerHour { get; init; }
}

/// <summary>Represents food available to an animal pen.</summary>
public sealed class FoodStorage
{
	/// <summary>Gets the current total level.</summary>
	public double Level { get; init; }

	/// <summary>Gets the total capacity.</summary>
	public double Capacity { get; init; }

	/// <summary>Gets the consumption rate per game hour.</summary>
	public double? PerHour { get; init; }

	/// <summary>Gets the food groups in the storage.</summary>
	public IList<FoodGroup> Groups { get; init; } = new List<FoodGroup>();
}

/// <summary>Represents one animal food group.</summary>
public sealed class FoodGroup
{
	/// <summary>Gets the group name.</summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>Gets the productivity weight.</summary>
	public double? Weight { get; init; }

	/// <summary>Gets the current level.</summary>
	public double Level { get; init; }

	/// <summary>Gets the accepted fill types.</summary>
	public IList<string> FillTypes { get; init; } = new List<string>();
}

/// <summary>Represents water available to an animal pen.</summary>
public sealed class WaterStorage
{
	/// <summary>Gets the current level.</summary>
	public double Level { get; init; }

	/// <summary>Gets the capacity.</summary>
	public double Capacity { get; init; }

	/// <summary>Gets the consumption rate per game hour.</summary>
	public double? PerHour { get; init; }

	/// <summary>Gets whether the water supply is automatically replenished.</summary>
	public bool IsAutomatic { get; init; }
}
