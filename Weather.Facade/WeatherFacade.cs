using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

using AutoMapper;

using Weather.AverageWeatherDataCalculator.Interfaces;
using Weather.Common.Dto;
using Weather.Common.Entities;
using Weather.DAL.Repository.Interface;

namespace Weather.Facade
{
    public class WeatherFacade : IWeatherFacade
    {
        private readonly IRepository repository;
        private readonly ICalculator calculator;

        public WeatherFacade(IRepository repository, ICalculator calculator)
        {
            this.repository = repository;
            this.calculator = calculator;
        }

        public IEnumerable<WorldDto> GetWorlds()
        {
            var worlds = this.repository.Get<WorldEntity>().ToList();

            return Mapper.Map<IEnumerable<WorldDto>>(worlds);
        }

        public IEnumerable<CountryDto> GetCountries(int worldId)
        {
            var countries = this.repository.Get<CountryEntity>(i => i.WorldId == worldId).ToList();

            return Mapper.Map<IEnumerable<CountryDto>>(countries);
        }

        public IEnumerable<RegionDto> GetRegiones(int countryId)
        {
            var regiones = this.repository.Get<RegionEntity>(i => i.CountryId == countryId).ToList();

            return Mapper.Map<IEnumerable<RegionDto>>(regiones);
        }

        public IEnumerable<CityDto> GetCities(int regionId)
        {
            var regiones = this.repository.Get<CityEntity>(i => i.RegionId == regionId).ToList();

            return Mapper.Map<IEnumerable<CityDto>>(regiones);
        }

        public IEnumerable<string> GetLastSevenDays(DateTime dateTime)
        {
            var days = this.repository
                .Get<WeatherDataEntity>(i => EntityFunctions.TruncateTime(i.DateTime) >= dateTime.Date)
                .Select(i => EntityFunctions.TruncateTime(i.DateTime))
                .Distinct()
                .OrderBy(i => i)
                .ToList()
                .Select(i => i.Value.ToString("yyyy-MM-dd"));

            return days;
        }

        public IEnumerable<WeatherDataDto> GetWeatherData(int cityId, DateTime dateTime, IEnumerable<int> providers)
        {
            var weatherData = this.repository
                .Get<WeatherDataEntity>(i => i.CityId == cityId && EntityFunctions.TruncateTime(i.DateTime) == dateTime.Date)
                .Join(providers, wd => (int)(wd.Provider), p => p, (wd, p) => wd)
                .GroupBy(wd => wd.DateTime)
                .ToList();

            var result = new List<WeatherDataDto>();

            foreach (var item in weatherData)
            {
                result.Add(this.GetAvgWeatherData(item.ToList()));
            }

            return result;
        }

        private WeatherDataDto GetAvgWeatherData(ICollection<WeatherDataEntity> weatherData)
        {
            if (weatherData.Count > 1)
            {
                var result = this.calculator.GetAvgWeatherData(Mapper.Map<IEnumerable<WeatherDataDto>>(weatherData));
                result.DateTime = weatherData.First().DateTime;

                return result;
            }

            return Mapper.Map<WeatherDataDto>(weatherData.First());
        }
    }
}