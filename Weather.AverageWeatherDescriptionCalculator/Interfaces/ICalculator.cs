
using System.Collections.Generic;

using Weather.Data.Entities;

namespace Weather.AverageWeatherDescriptionCalculator.Interfaces
{
    public interface ICalculator
    {
        WeatherDescription GetAvgWeatherDescription(IEnumerable<WeatherDescription> weatherDescriptions);
    }
}
