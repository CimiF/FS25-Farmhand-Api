using FarmStateStore = FS25Gateway.Application.StateStore.StateStore;
using FS25Gateway.Application.StateStore;
using FS25Gateway.Domain.Models;
using FS25Gateway.Infrastructure.Telemetry.Frames;

namespace FS25Gateway.Application.Services.Mappers;

/// <summary>Maps sites frames into production and animal pen state.</summary>
public sealed class SitesFrameMapper(FarmStateStore stateStore)
{
	/// <summary>Applies a sites frame to the existing farm aggregate.</summary>
	/// <param name="frame">The sites frame to apply.</param>
	public void Map(SitesFrame frame)
	{
		ArgumentNullException.ThrowIfNull(frame);
		var changes = new List<FarmStateChange>();

		stateStore.Update(state =>
		{
			var productions = frame.Productions.ToDictionary(item => item.Id, MapProduction);
			state.Productions.Clear();
			foreach (var pair in productions)
			{
				state.Productions[pair.Key] = pair.Value;
				changes.Add(new ProductionUpdatedChange(pair.Value));
			}

			var pens = frame.Pens.ToDictionary(item => item.Id, MapPen);
			state.AnimalPens.Clear();
			foreach (var pair in pens)
			{
				state.AnimalPens[pair.Key] = pair.Value;
			}
		}, changes);
	}

	private static ProductionSite MapProduction(ProductionSiteFrameItem item) => new()
	{
		Id = item.Id,
		Name = item.Name ?? string.Empty,
		IsEnabled = item.On,
		StopReason = MapperConversion.ToStopReason(item.Stopped),
		Lines = item.Lines.Select(line => new ProductionLine
		{
			Name = line.Name ?? string.Empty,
			IsEnabled = line.On,
			Status = line.Status,
			CyclesPerHour = line.CyclesPerHour
		}).ToList(),
		Inputs = item.Inputs.Select(input => new InputBuffer
		{
			FillType = input.Fill ?? string.Empty,
			Level = input.Lvl,
			Capacity = input.Cap,
			PerHour = input.PerHour
		}).ToList(),
		Outputs = item.Outputs.Select(output => new OutputBuffer
		{
			FillType = output.Fill ?? string.Empty,
			Mode = output.Mode,
			Level = output.Lvl,
			Capacity = output.Cap,
			PerHour = output.PerHour
		}).ToList()
	};

	private static AnimalPen MapPen(AnimalPenFrameItem item) => new()
	{
		Id = item.Id,
		Name = item.Name ?? string.Empty,
		AnimalType = item.Type,
		Count = item.Count,
		Maximum = item.Max,
		Productivity = item.Productivity,
		Health = item.Health,
		Food = item.Food is null ? null : new FoodStorage
		{
			Level = item.Food.Lvl,
			Capacity = item.Food.Cap,
			PerHour = item.Food.PerHour,
			Groups = item.Food.Groups.Select(group => new FoodGroup
			{
				Name = group.Name ?? string.Empty,
				Weight = group.Weight,
				Level = group.Lvl,
				FillTypes = group.Fills.ToList()
			}).ToList()
		},
		Water = item.Water is null ? null : new WaterStorage
		{
			Level = item.Water.Lvl,
			Capacity = item.Water.Cap,
			PerHour = item.Water.PerHour,
			IsAutomatic = item.Water.Auto
		},
		Outputs = item.Outputs.Select(output => new AnimalOutput
		{
			FillType = output.Fill ?? string.Empty,
			Kind = output.Kind,
			Level = output.Lvl,
			Capacity = output.Cap,
			PerHour = output.PerHour
		}).ToList()
	};
}
