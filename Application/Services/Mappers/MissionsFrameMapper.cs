using FarmStateStore = FS25Gateway.Application.StateStore.StateStore;
using FS25Gateway.Application.StateStore;
using FS25Gateway.Domain.Models;
using FS25Gateway.Infrastructure.Telemetry.Frames;

namespace FS25Gateway.Application.Services.Mappers;

/// <summary>Maps mission frames into the contract dictionary.</summary>
public sealed class MissionsFrameMapper(FarmStateStore stateStore)
{
	/// <summary>Applies a missions frame to the existing farm aggregate.</summary>
	/// <param name="frame">The missions frame to apply.</param>
	public void Map(MissionsFrame frame)
	{
		ArgumentNullException.ThrowIfNull(frame);
		var changes = new List<FarmStateChange>();

		stateStore.Update(state =>
		{
			var contracts = frame.Items
				.Where(item => !string.IsNullOrWhiteSpace(item.Id))
				.ToDictionary(item => item.Id!, MapContract, StringComparer.Ordinal);

			state.Contracts.Clear();
			foreach (var pair in contracts)
			{
				state.Contracts[pair.Key] = pair.Value;
				changes.Add(new ContractUpdatedChange(pair.Value));
			}
		}, changes);
	}

	private static Contract MapContract(MissionFrameItem item) => new()
	{
		Id = item.Id ?? string.Empty,
		Type = item.Type,
		Title = item.Title,
		Description = item.Desc,
		Status = MapperConversion.ToContractStatus(item.Status),
		IsOwned = item.Mine,
		Reward = item.Reward,
		Completion = item.Completion,
		FieldId = item.Field,
		Position = MapperConversion.ToPosition(item.X, item.Z),
		Deadline = MapperConversion.ToGameDate(item.EndDay, item.EndDayTime)
	};
}
