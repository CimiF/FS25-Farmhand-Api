namespace FS25Gateway.Domain.Enums;

/// <summary>Describes the current lifecycle state of a field.</summary>
public enum FieldState
{
	Fallow,
	Sown,
	Growing,
	Ready,
	Withered,
	Harvested
}

/// <summary>Describes the lifecycle state of a contract.</summary>
public enum ContractStatus
{
	Created,
	Preparing,
	Running,
	Finished,
	Dismissed
}

/// <summary>Describes the severity of a farm alert.</summary>
public enum AlertSeverity
{
	Information,
	Warning,
	Critical
}

/// <summary>Describes the current state of a vehicle worker.</summary>
public enum WorkerStatus
{
	Idle,
	Working,
	Stuck,
	Stopped,
	Finished
}

/// <summary>Identifies the system controlling a vehicle worker.</summary>
public enum VehicleWorkerKind
{
	Ai,
	Courseplay,
	AutoDrive
}

/// <summary>Describes why a production site is stopped.</summary>
public enum ProductionStopReason
{
	None,
	Off,
	InputEmpty,
	OutputFull,
	Unknown
}
