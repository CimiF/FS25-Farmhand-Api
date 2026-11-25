using FarmStateStore = FS25Gateway.Application.StateStore.StateStore;
using FS25Gateway.Application.StateStore;
using FS25Gateway.Domain.Models;
using FS25Gateway.Infrastructure.Telemetry.Frames;

namespace FS25Gateway.Application.Services.Mappers;

/// <summary>Maps storage frames into storage locations and inventory totals.</summary>
public sealed class StorageFrameMapper(FarmStateStore stateStore)
{
	/// <summary>Applies a storage frame to the existing farm aggregate.</summary>
	/// <param name="frame">The storage frame to apply.</param>
	public void Map(StorageFrame frame)
	{
		ArgumentNullException.ThrowIfNull(frame);
		var changes = new List<FarmStateChange>();

		stateStore.Update(state =>
		{
			var storages = frame.Items.ToDictionary(item => item.Id, item => new StorageLocation
			{
				Id = item.Id,
				Name = item.Name,
				Capacity = item.Capacity,
				Fills = item.Fills.Select(MapperConversion.ToFillLevel).ToList()
			});

			state.Storages.Clear();
			foreach (var pair in storages)
			{
				state.Storages[pair.Key] = pair.Value;
				changes.Add(new StorageUpdatedChange(pair.Value));
			}

			state.Inventory.Fills.Clear();
			foreach (var total in frame.Totals.Where(item => !string.IsNullOrWhiteSpace(item.Fill)))
			{
				state.Inventory.Fills[total.Fill!] = total.Lvl;
			}

			state.Inventory.Pallets.Clear();
			foreach (var pallet in frame.Pallets.Where(item => !string.IsNullOrWhiteSpace(item.Fill)))
			{
				state.Inventory.Pallets[pallet.Fill!] = pallet.Lvl;
			}
		}, changes);
	}
}
