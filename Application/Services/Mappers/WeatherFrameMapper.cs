using FarmStateStore = FS25Gateway.Application.StateStore.StateStore;
using FS25Gateway.Application.StateStore;
using FS25Gateway.Domain.Models;
using FS25Gateway.Infrastructure.Telemetry.Frames;

namespace FS25Gateway.Application.Services.Mappers;

/// <summary>Maps weather frames into the weather aggregate state.</summary>
public sealed class WeatherFrameMapper(FarmStateStore stateStore)
{
	/// <summary>Applies a weather frame to the existing farm aggregate.</summary>
	/// <param name="frame">The weather frame to apply.</param>
	public void Map(WeatherFrame frame)
	{
		ArgumentNullException.ThrowIfNull(frame);
		var changes = new List<FarmStateChange>();

		stateStore.Update(state =>
		{
			state.Weather = new WeatherState
			{
				CurrentType = frame.Now?.ToString(),
				Temperature = frame.Temp,
				MinimumTemperature = frame.Min,
				MaximumTemperature = frame.Max,
				IsRaining = frame.Raining,
				RainInMinutes = frame.RainInMin,
				Forecast = frame.Items.Select(item => new ForecastItem
				{
					WeatherType = item.Type,
					Day = item.Day,
					Hour = item.Hour,
					Minute = item.Minute,
					DurationMinutes = item.DurationMin
				}).ToList()
			};
			changes.Add(new WeatherUpdatedChange(state.Weather));
		}, changes);
	}
}
