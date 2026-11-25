using FarmStateStore = FS25Gateway.Application.StateStore.StateStore;
using FS25Gateway.Domain.Models;
using FS25Gateway.Infrastructure.Telemetry.Frames;

namespace FS25Gateway.Application.Services.Mappers;

/// <summary>Maps players frames into the player aggregate state.</summary>
public sealed class PlayersFrameMapper(FarmStateStore stateStore)
{
	/// <summary>Applies a players frame to the existing farm aggregate.</summary>
	/// <param name="frame">The players frame to apply.</param>
	public void Map(PlayersFrame frame)
	{
		ArgumentNullException.ThrowIfNull(frame);

		stateStore.Update(state =>
		{
			state.Players = new PlayerState
			{
				IsMultiplayer = frame.Multiplayer,
				IsServer = frame.Server,
				IsMaster = frame.Master,
				CurrentPlayerId = frame.Me,
				Users = frame.Users.Select(user => new Player
				{
					Id = user.Id,
					Name = user.Name ?? string.Empty,
					Farm = user.Farm,
					IsMaster = user.Master
				}).ToList()
			};
		});
	}
}
