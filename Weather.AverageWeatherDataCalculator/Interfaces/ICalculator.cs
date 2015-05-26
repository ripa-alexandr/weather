
using System.Collections.Generic;

using Weather.Common.Dto;

namespace Weather.AverageWeatherDataCalculator.Interfaces
{
    public interface ICalculator
    {
        WeatherDataAggregateDto GetAvgWeatherData(IEnumerable<WeatherDataDto> weatherData);
    }
}
