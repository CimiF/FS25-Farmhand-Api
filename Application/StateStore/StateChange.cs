using FS25Gateway.Domain.Models;

namespace FS25Gateway.Application.StateStore;

/// <summary>Represents one typed change committed to the farm aggregate.</summary>
public abstract record FarmStateChange;

/// <summary>Indicates that a vehicle was updated.</summary>
public sealed record VehicleUpdatedChange(Vehicle Vehicle) : FarmStateChange;

/// <summary>Indicates that a worker was updated.</summary>
public sealed record WorkerUpdatedChange(long VehicleId, WorkerState Worker) : FarmStateChange;

/// <summary>Indicates that a field was updated.</summary>
public sealed record FieldUpdatedChange(Field Field) : FarmStateChange;

/// <summary>Indicates that a contract was updated.</summary>
public sealed record ContractUpdatedChange(Contract Contract) : FarmStateChange;

/// <summary>Indicates that a production site was updated.</summary>
public sealed record ProductionUpdatedChange(ProductionSite Production) : FarmStateChange;

/// <summary>Indicates that a storage location was updated.</summary>
public sealed record StorageUpdatedChange(StorageLocation Storage) : FarmStateChange;

/// <summary>Indicates that the weather state was updated.</summary>
public sealed record WeatherUpdatedChange(WeatherState Weather) : FarmStateChange;

/// <summary>Indicates that a new alert was created.</summary>
public sealed record AlertCreatedChange(Alert Alert) : FarmStateChange;
