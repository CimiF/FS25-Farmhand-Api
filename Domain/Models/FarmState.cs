namespace FS25Gateway.Domain.Models;

/// <summary>Represents the long-lived aggregate state of the farm.</summary>
public sealed class FarmState
{
	/// <summary>Gets or sets the time at which this aggregate was last updated.</summary>
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

	/// <summary>Gets the current game-world information.</summary>
	public WorldInfo World { get; init; } = new();

	/// <summary>Gets vehicles keyed by their game identifier.</summary>
	public IDictionary<long, Vehicle> Vehicles { get; init; } = new Dictionary<long, Vehicle>();

	/// <summary>Gets fields keyed by their field number.</summary>
	public IDictionary<int, Field> Fields { get; init; } = new Dictionary<int, Field>();

	/// <summary>Gets contracts keyed by their unique identifier.</summary>
	public IDictionary<string, Contract> Contracts { get; init; } = new Dictionary<string, Contract>(StringComparer.Ordinal);

	/// <summary>Gets production sites keyed by their game identifier.</summary>
	public IDictionary<long, ProductionSite> Productions { get; init; } = new Dictionary<long, ProductionSite>();

	/// <summary>Gets animal pens keyed by their game identifier.</summary>
	public IDictionary<long, AnimalPen> AnimalPens { get; init; } = new Dictionary<long, AnimalPen>();

	/// <summary>Gets storage locations keyed by their game identifier.</summary>
	public IDictionary<long, StorageLocation> Storages { get; init; } = new Dictionary<long, StorageLocation>();

	/// <summary>Gets sell stations keyed by their station identifier.</summary>
	public IDictionary<int, SellStation> Stations { get; init; } = new Dictionary<int, SellStation>();

	/// <summary>Gets the current weather state.</summary>
	public WeatherState Weather { get; set; } = new();

	/// <summary>Gets the current save settings.</summary>
	public GameSettings Settings { get; set; } = new();

	/// <summary>Gets the current player state.</summary>
	public PlayerState Players { get; set; } = new();

	/// <summary>Gets aggregate inventory totals.</summary>
	public InventoryTotals Inventory { get; init; } = new();

	/// <summary>Gets the current farm alerts.</summary>
	public AlertsState Alerts { get; init; } = new();
}
