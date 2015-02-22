
using System.Collections.Generic;

using Weather.Common.Entities;

namespace Weather.AverageWeatherDescriptionCalculator.Interfaces
{
    public interface ICalculator
    {
        WeatherDescriptionEntity GetAvgWeatherDescription(IEnumerable<WeatherDescriptionEntity> weatherDescriptions);
    }
}
