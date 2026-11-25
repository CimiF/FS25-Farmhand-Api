using System.Text.Json.Serialization;

namespace FS25Gateway.Web.Models;

/// <summary>Represents the dashboard snapshot returned by the gateway API.</summary>
public sealed record DashboardSnapshotDto
{
	public DateTime UpdatedAt { get; init; }
	public WeatherDto? Weather { get; init; }
	public IReadOnlyDictionary<long, VehicleDto> Vehicles { get; init; } = new Dictionary<long, VehicleDto>();
	public IReadOnlyDictionary<string, ContractDto> Contracts { get; init; } = new Dictionary<string, ContractDto>();
	public AlertsStateDto? Alerts { get; init; }
}

/// <summary>Represents current weather conditions and forecast.</summary>
public sealed record WeatherDto
{
	public string? CurrentType { get; init; }
	public double? Temperature { get; init; }
	public double? MinimumTemperature { get; init; }
	public double? MaximumTemperature { get; init; }
	public bool IsRaining { get; init; }
	public int? RainInMinutes { get; init; }
	public IReadOnlyList<ForecastDto> Forecast { get; init; } = Array.Empty<ForecastDto>();
}

/// <summary>Represents one weather forecast item.</summary>
public sealed record ForecastDto
{
	public string? WeatherType { get; init; }
	public int Day { get; init; }
	public int Hour { get; init; }
	public int Minute { get; init; }
	public int DurationMinutes { get; init; }
}

/// <summary>Represents a vehicle shown on the dashboard.</summary>
public sealed record VehicleDto
{
	public long Id { get; init; }
	public string Name { get; init; } = string.Empty;
	public VehiclePositionDto? Position { get; init; }
	public WorkerDto? Worker { get; init; }
	public double? FuelPercent { get; init; }
	public double? Damage { get; init; }
}

/// <summary>Represents the worker assigned to a vehicle.</summary>
public sealed record WorkerDto
{
	public string? Status { get; init; }
	public string? Kind { get; init; }
	public string? JobType { get; init; }
	public string? Job { get; init; }
	public int? FieldId { get; init; }
	public TimeSpan? StillFor { get; init; }
}

/// <summary>Represents a vehicle or contract position.</summary>
public sealed record VehiclePositionDto
{
	public double X { get; init; }
	public double Z { get; init; }
	public int? FieldId { get; init; }
}

/// <summary>Represents a farm contract.</summary>
public sealed record ContractDto
{
	public string Id { get; init; } = string.Empty;
	public string? Type { get; init; }
	public string? Title { get; init; }
	public string? Description { get; init; }
	public string? Status { get; init; }
	public bool IsOwned { get; init; }
	public decimal Reward { get; init; }
	public double? Completion { get; init; }
	public int? FieldId { get; init; }
}

/// <summary>Represents the dashboard alert collection.</summary>
public sealed record AlertsStateDto
{
	public IReadOnlyList<AlertDto> Items { get; init; } = Array.Empty<AlertDto>();
}

/// <summary>Represents one dashboard alert.</summary>
public sealed record AlertDto
{
	public Guid Id { get; init; }
	public string? Severity { get; init; }
	public string? Code { get; init; }
	public string Message { get; init; } = string.Empty;
	public DateTime CreatedAt { get; init; }
	public bool IsAcknowledged { get; init; }
}
