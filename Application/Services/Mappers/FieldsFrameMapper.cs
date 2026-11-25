using FarmStateStore = FS25Gateway.Application.StateStore.StateStore;
using FS25Gateway.Application.StateStore;
using FS25Gateway.Domain.Models;
using FS25Gateway.Infrastructure.Telemetry.Frames;

namespace FS25Gateway.Application.Services.Mappers;

/// <summary>Maps fields frames into the farm field dictionary.</summary>
public sealed class FieldsFrameMapper(FarmStateStore stateStore)
{
	/// <summary>Applies a fields frame to the existing farm aggregate.</summary>
	/// <param name="frame">The fields frame to apply.</param>
	public void Map(FieldsFrame frame)
	{
		ArgumentNullException.ThrowIfNull(frame);
		var changes = new List<FarmStateChange>();

		stateStore.Update(state =>
		{
			var mapped = frame.Rows.ToDictionary(item => item.N, MapField);
			state.Fields.Clear();
			foreach (var pair in mapped)
			{
				state.Fields[pair.Key] = pair.Value;
				changes.Add(new FieldUpdatedChange(pair.Value));
			}

		}, changes);
	}

	private static Field MapField(FieldFrameItem item) => new()
	{
		Id = item.N,
		FarmlandId = item.Farmland,
		Hectares = item.Ha,
		Position = MapperConversion.ToPosition(item.X, item.Z),
		Crop = item.Crop,
		State = MapperConversion.ToFieldState(item.State),
		Growth = item.Growth,
		GrowthMaximum = item.GrowthMax,
		Closing = item.Closing,
		Needs = item.Needs.ToList(),
		Soil = item.Soil is null ? new SoilState() : new SoilState
		{
			GrowthState = item.Soil.GrowthState,
			LastGrowthState = item.Soil.LastGrowthState,
			Lime = item.Soil.Lime,
			Plow = item.Soil.Plow,
			Roller = item.Soil.Roller,
			Spray = item.Soil.Spray,
			SprayType = item.Soil.SprayType,
			Stones = item.Soil.Stones,
			StubbleShred = item.Soil.StubbleShred,
			Weed = item.Soil.Weed,
			WeedFactor = item.Soil.WeedFactor,
			Water = item.Soil.Water,
			Ground = item.Soil.Ground
		}
	};
}
