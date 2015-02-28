
using System.Collections.Generic;

using Weather.Common.Dto;

namespace Weather.AverageWeatherDescriptionCalculator.Interfaces
{
    public interface ICalculator
    {
        WeatherDescriptionDto GetAvgWeatherDescription(IEnumerable<WeatherDescriptionDto> weatherDescriptions);
    }
}
