
using System;
using System.Collections.Generic;

using Weather.Common.Dto;

namespace Weather.Facade
{
    public interface IWeatherFacade
    {
        IEnumerable<WorldDto> GetWorlds();

        IEnumerable<CountryDto> GetCountries(int worldId);

        IEnumerable<RegionDto> GetRegiones(int countryId);

        IEnumerable<CityDto> GetCities(int regionId);

        IEnumerable<string> GetLastSevenDays(int cityId, DateTime dateTime, IEnumerable<int> providers);

        IEnumerable<WeatherDataDto> GetWeatherData(int cityId, DateTime dateTime, IEnumerable<int> providers);
    }
}
