
using System;
using System.Collections.Generic;

using Weather.Common.Dto;

namespace Weather.Facade
{
    public interface IWeatherFacade
    {
        IEnumerable<WeatherDataDto> GetAvgWeatherData(int cityId, DateTime dateTime, IEnumerable<int> providers);
    }
}
