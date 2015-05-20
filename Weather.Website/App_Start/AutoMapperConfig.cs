
using System;
using System.Text;

using AutoMapper;

using Weather.Bootstrap;
using Weather.Common.Dto;
using Weather.Common.Enums;
using Weather.Common.Extensions;
using Weather.Website.Models;
using Weather.Website.Resources;

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
            Mapper.CreateMap<WorldDto, WorldViewModel>();
            Mapper.CreateMap<CountryDto, CountryViewModel>();
            Mapper.CreateMap<RegionDto, RegionViewModel>();
            Mapper.CreateMap<CityDto, CityViewModel>();
            Mapper.CreateMap<WeatherDataDto, WeatherDataViewModel>()
                .ForMember(d => d.TimeOfDay, opt => opt.MapFrom(i => GetTimeOfDay(i.DateTime)))
                .ForMember(d => d.Cloudy, opt => opt.MapFrom(i => GetCloudy(i.Cloudy)))
                .ForMember(d => d.Precipitation, opt => opt.MapFrom(i => GetPrecipitation(i.Precipitation, i.StrengthPrecipitation, i.IsFog, i.IsThunderstorm)))
                .ForMember(d => d.WindDirection, opt => opt.MapFrom(i => GetWindDirection(i.WindDirection)));
        }

        private static string GetTimeOfDay(DateTime dateTime)
        {
            switch (dateTime.ToString("HH:mm:ss"))
            {
                case "03:00:00":
                    return AppResource.TimeOfDay_Night;

                case "09:00:00":
                    return AppResource.TimeOfDay_Morning;

                case "15:00:00":
                    return AppResource.TimeOfDay_Midday;

                case "21:00:00":
                    return AppResource.TimeOfDay_Evening;
            }

            return string.Empty;
        }

        private static string GetPrecipitation(PrecipitationType precipitation, StrengthPrecipitationType strengthPrecipitation, bool isFog, bool isThunderstorm)
        {
            var sb = new StringBuilder();
            sb.Append(GetStrengthPrecipitation(strengthPrecipitation));
            sb.Append(GetPrecipitation(precipitation));
            sb.AppendFormat(isThunderstorm ? ", {0}".F(AppResource.WeatherData_Thunderstorm.ToLower()) : "");
            sb.Append(isFog ? ", {0}".F(AppResource.WeatherData_Fog) : "");
            
            return sb.ToString();
        }

        private static string GetStrengthPrecipitation(StrengthPrecipitationType strengthPrecipitation)
        {
            switch (strengthPrecipitation)
            {
                case StrengthPrecipitationType.Light:
                    return AppResource.StrengthPrecipitationType_Light;

                case StrengthPrecipitationType.Heavy:
                    return AppResource.StrengthPrecipitationType_Heavy;
            }

            return string.Empty;
        }

        private static string GetPrecipitation(PrecipitationType precipitation)
        {
            switch (precipitation)
            {
                case PrecipitationType.Rain:
                    return AppResource.PrecipitationType_Rain;

                case PrecipitationType.Sleet:
                    return AppResource.PrecipitationType_Sleet;

                case PrecipitationType.Snow:
                    return AppResource.PrecipitationType_Snow;
            }

            return string.Empty;
        }

        private static string GetWindDirection(WindDirectionType direction)
        {
            switch (direction)
            {
                case WindDirectionType.North:
                    return AppResource.WindDirectionType_North;

                case WindDirectionType.NorthEast:
                    return AppResource.WindDirectionType_NorthEast;

                case WindDirectionType.East:
                    return AppResource.WindDirectionType_East;

                case WindDirectionType.SouthEast:
                    return AppResource.WindDirectionType_SouthEast;

                case WindDirectionType.South:
                    return AppResource.WindDirectionType_South;

                case WindDirectionType.SouthWest:
                    return AppResource.WindDirectionType_SouthWest;

                case WindDirectionType.West:
                    return AppResource.WindDirectionType_West;

                case WindDirectionType.NorthWest:
                    return AppResource.WindDirectionType_NorthWest;
            }

            return string.Empty;
        }

        private static string GetCloudy(CloudyType cloudy)
        {
            switch (cloudy)
            {
                case CloudyType.Fair:
                    return AppResource.CloudyType_Fair;

                case CloudyType.PartlyCloudy:
                    return AppResource.CloudyType_PartlyCloudy;

                case CloudyType.Cloudy:
                    return AppResource.CloudyType_Cloudy;

                case CloudyType.MainlyCloudy:
                    return AppResource.CloudyType_MainlyCloudy;

                case CloudyType.Overcast:
                    return AppResource.CloudyType_Overcast;
            }

            return string.Empty;
        }
    }
}