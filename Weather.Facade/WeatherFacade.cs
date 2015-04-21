using System;
using System.Collections.Generic;
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

        public IEnumerable<WeatherDataDto> GetAvgWeatherData(int cityId, DateTime dateTime, IEnumerable<int> providers)
        {
            var weatherData = this.repository
                .Get<WeatherDataEntity>(i => i.CityId == cityId 
                    && i.DateTime.Year == dateTime.Year && i.DateTime.Month == dateTime.Month && i.DateTime.Day == dateTime.Day)
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