using FS25Gateway.Domain.Enums;
using FS25Gateway.Domain.Models;
using FS25Gateway.Infrastructure.Telemetry.Frames;

namespace FS25Gateway.Application.Services.Mappers;

internal static class MapperConversion
{
	public static double Level(FillFrameItem item) => item.Level != 0 ? item.Level : item.Lvl;

	public static double Capacity(FillFrameItem item) => item.Capacity != 0 ? item.Capacity : item.Cap;

	public static FillLevel ToFillLevel(FillFrameItem item) => new()
	{
		FillType = item.FillType ?? string.Empty,
		Level = Level(item),
		Capacity = Capacity(item)
	};

	public static VehiclePosition? ToPosition(double? x, double? z, int? fieldId = null) =>
		x.HasValue && z.HasValue
			? new VehiclePosition { X = x.Value, Z = z.Value, FieldId = fieldId }
			: null;

	public static FieldState ToFieldState(string? value) =>
		value?.ToLowerInvariant() switch
		{
			"sown" => FieldState.Sown,
			"growing" => FieldState.Growing,
			"ready" => FieldState.Ready,
			"withered" => FieldState.Withered,
			"harvested" => FieldState.Harvested,
			_ => FieldState.Fallow
		};

	public static ContractStatus ToContractStatus(string? value) =>
		value?.ToLowerInvariant() switch
		{
			"preparing" => ContractStatus.Preparing,
			"running" => ContractStatus.Running,
			"finished" => ContractStatus.Finished,
			"dismissed" => ContractStatus.Dismissed,
			_ => ContractStatus.Created
		};

	public static WorkerStatus ToWorkerStatus(string? value) =>
		value?.ToLowerInvariant() switch
		{
			"working" => WorkerStatus.Working,
			"stuck" => WorkerStatus.Stuck,
			"stopped" => WorkerStatus.Stopped,
			"finished" => WorkerStatus.Finished,
			_ => WorkerStatus.Idle
		};

	public static VehicleWorkerKind ToWorkerKind(string? value) =>
		value?.ToLowerInvariant() switch
		{
			"courseplay" => VehicleWorkerKind.Courseplay,
			"autodrive" => VehicleWorkerKind.AutoDrive,
			_ => VehicleWorkerKind.Ai
		};

	public static ProductionStopReason? ToStopReason(string? value) =>
		value?.ToLowerInvariant() switch
		{
			null or "" => null,
			"off" => ProductionStopReason.Off,
			"inputempty" => ProductionStopReason.InputEmpty,
			"outputfull" => ProductionStopReason.OutputFull,
			_ => ProductionStopReason.Unknown
		};

	public static DateTime? ToGameDate(int? day, int? dayTime)
	{
		if (!day.HasValue || !dayTime.HasValue)
		{
			return null;
		}

		return DateTime.UnixEpoch.AddDays(Math.Max(0, day.Value - 1)).AddMilliseconds(dayTime.Value);
	}
}
