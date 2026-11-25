namespace FS25Gateway.Domain.Models;

/// <summary>Represents one storage location owned by the farm.</summary>
public sealed class StorageLocation
{
	/// <summary>Gets the network identifier.</summary>
	public long Id { get; init; }

	/// <summary>Gets the storage name.</summary>
	public string? Name { get; init; }

	/// <summary>Gets the total capacity.</summary>
	public double? Capacity { get; init; }

	/// <summary>Gets the fill levels in this storage.</summary>
	public IList<FillLevel> Fills { get; init; } = new List<FillLevel>();
}

/// <summary>Represents a sell station.</summary>
public sealed class SellStation
{
	/// <summary>Gets the station identifier.</summary>
	public int Id { get; init; }

	/// <summary>Gets the station name.</summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>Gets the station position.</summary>
	public VehiclePosition? Position { get; init; }

	/// <summary>Gets the prices offered by the station.</summary>
	public IList<SellPrice> Prices { get; init; } = new List<SellPrice>();
}

/// <summary>Represents one price offered by a sell station.</summary>
public sealed class SellPrice
{
	/// <summary>Gets the fill type.</summary>
	public string FillType { get; init; } = string.Empty;

	/// <summary>Gets the price per litre.</summary>
	public decimal Price { get; init; }

	/// <summary>Gets the optional price trend.</summary>
	public double? Trend { get; init; }

	/// <summary>Gets the optional demand multiplier.</summary>
	public double? Demand { get; init; }
}
