using FarmStateStore = FS25Gateway.Application.StateStore.StateStore;
using FS25Gateway.Domain.Models;
using FS25Gateway.Infrastructure.Telemetry.Frames;

namespace FS25Gateway.Application.Services.Mappers;

/// <summary>Maps settings frames into the game settings aggregate state.</summary>
public sealed class SettingsFrameMapper(FarmStateStore stateStore)
{
	/// <summary>Applies a settings frame to the existing farm aggregate.</summary>
	/// <param name="frame">The settings frame to apply.</param>
	public void Map(SettingsFrame frame)
	{
		ArgumentNullException.ThrowIfNull(frame);

		stateStore.Update(state =>
		{
			var settings = new GameSettings
			{
				LimeRequired = frame.Needs?.Lime ?? false,
				PlowingRequired = frame.Needs?.Plow ?? false,
				WeedsEnabled = frame.Needs?.Weeds ?? false,
				StonesEnabled = frame.Needs?.Stones ?? false,
				DaysPerPeriod = frame.DaysPerPeriod,
				GrowthMode = frame.GrowthMode,
				FuelUsage = frame.FuelUsage,
				EconomicDifficulty = frame.EconomicDifficulty,
				SnowEnabled = frame.Snow ?? false
			};

			if (frame.Max is not null)
			{
				foreach (var pair in frame.Max)
				{
					settings.MaximumSoilValues[pair.Key] = pair.Value;
				}
			}

			if (frame.Crops is not null)
			{
				foreach (var pair in frame.Crops)
				{
					settings.Crops[pair.Key] = new CropSettings
					{
						PlantingPeriods = pair.Value.Plant.ToList(),
						HarvestPeriods = pair.Value.Harvest.ToList(),
						NeedsRolling = pair.Value.NeedsRolling,
						GrowthRequiresLime = pair.Value.GrowthRequiresLime,
						PlantsWeeds = pair.Value.PlantsWeed
					};
				}
			}

			state.Settings = settings;
		});
	}
}
