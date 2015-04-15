﻿
using AutoMapper;

using Weather.Bootstrap;
using Weather.Common.Dto;
using Weather.Website.Models;

namespace Weather.Website
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            AutoMapperConfiguration.Configure();

            ConfigureDtoToViewModel();
        }

        private static void ConfigureDtoToViewModel()
        {
            Mapper.CreateMap<CityDto, CityViewModel>();
            Mapper.CreateMap<WeatherDataDto, WeatherDataViewModel>();
        }
    }
}