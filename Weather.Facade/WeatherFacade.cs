using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Weather.AverageWeatherDataCalculator.Interfaces;
using Weather.Common.Dto;
using Weather.Common.Entities;
using Weather.Common.Enums;
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

        public WeatherDataDto GetAvgWeatherData(int cityId, DateTime dateTime, IEnumerable<int> providers)
        {
            var weatherData = this.repository.Get<WeatherDataEntity>(i => i.CityId == cityId && i.DateTime == dateTime)
                .ToList()
                .Where(i => providers.Any(p => (ProviderType)p == i.Provider))
                .ToList();

            return this.GetAvgWeatherData(weatherData);
        }

        private WeatherDataDto GetAvgWeatherData(ICollection<WeatherDataEntity> weatherData)
        {
            if (weatherData.Count > 1)
            {
                return this.calculator.GetAvgWeatherData(Mapper.Map<IEnumerable<WeatherDataDto>>(weatherData));
            }

            return Mapper.Map<WeatherDataDto>(weatherData.First());
        }
    }
}