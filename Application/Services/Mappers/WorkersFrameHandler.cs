using FarmStateStore = FS25Gateway.Application.StateStore.StateStore;
using FS25Gateway.Application.StateStore;
using FS25Gateway.Domain.Models;
using FS25Gateway.Infrastructure.Telemetry.Frames;
using FS25Gateway.Infrastructure.Telemetry.Handlers;
using Microsoft.Extensions.Logging;

namespace FS25Gateway.Application.Services.Mappers;

/// <summary>Maps workers frames into the farm vehicle aggregate.</summary>
public sealed class WorkersFrameHandler(
	FarmStateStore stateStore,
	ILogger<WorkersFrameHandler> logger) : IWorkersFrameHandler
{
	/// <summary>Handles a workers frame asynchronously.</summary>
	public Task HandleAsync(WorkersFrame frame, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(frame);
		cancellationToken.ThrowIfCancellationRequested();
		Map(frame);
		cancellationToken.ThrowIfCancellationRequested();
		return Task.CompletedTask;
	}

	/// <summary>Maps a workers frame into the existing farm state.</summary>
	/// <param name="frame">The workers frame to map.</param>
	public void Map(WorkersFrame frame)
	{
		ArgumentNullException.ThrowIfNull(frame);
		var changes = new List<FarmStateChange>();

		logger.LogInformation("Mapping workers frame with {WorkerCount} workers.", frame.Items.Count);

		stateStore.Update(state =>
		{
			foreach (var item in frame.Items)
			{
				var isNewVehicle = !state.Vehicles.TryGetValue(item.Id, out var vehicle);
				if (isNewVehicle)
				{
					vehicle = new Vehicle { Id = item.Id };
					state.Vehicles[item.Id] = vehicle;
					logger.LogDebug("Created vehicle {VehicleId} from workers frame.", item.Id);
				}
				else
				{
					logger.LogDebug("Updating vehicle {VehicleId} from workers frame.", item.Id);
				}

				vehicle.Name = item.Name ?? vehicle.Name;
				vehicle.Position = MapperConversion.ToPosition(item.X, item.Z, item.Field);
				vehicle.Worker = new WorkerState
				{
					Status = MapperConversion.ToWorkerStatus(item.State),
					Kind = MapperConversion.ToWorkerKind(item.Kind),
					JobType = item.JobType,
					Job = item.Job,
					FieldId = item.Field,
					StillFor = item.StillMs.HasValue ? TimeSpan.FromMilliseconds(item.StillMs.Value) : null,
					StopReason = item.StopReason
				};
				vehicle.FuelPercent = item.Fuel;
				vehicle.Damage = item.Damage;

				vehicle.Fills.Clear();
				foreach (var fill in item.Fills)
				{
					vehicle.Fills.Add(MapperConversion.ToFillLevel(fill));
				}

				vehicle.Implements.Clear();
				foreach (var implement in item.Implements)
				{
					vehicle.Implements.Add(new Implement
					{
						Name = implement.Name ?? string.Empty,
						Damage = implement.Damage,
						Fills = implement.Fills.Select(MapperConversion.ToFillLevel).ToList()
					});
				}

				changes.Add(new VehicleUpdatedChange(vehicle));
				changes.Add(new WorkerUpdatedChange(vehicle.Id, vehicle.Worker));

				logger.LogDebug(
					"Mapped worker {VehicleId} with status {WorkerStatus} and kind {WorkerKind}.",
					vehicle.Id,
					vehicle.Worker.Status,
					vehicle.Worker.Kind);
			}
		}, changes);
	}
}
