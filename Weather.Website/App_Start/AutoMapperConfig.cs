
using System;
using System.Linq;
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
            
            Mapper.CreateMap<WeatherDataAggregateDto, WeatherDataAggregateViewModel>()
                .ConvertUsing(source => new WeatherDataAggregateViewModel
                    {
                        Providers = source.Providers.Select(p => p.Provider),
                        DateTime = source.Providers.Select(p => p.DateTime).Distinct().Single(),
                        TimeOfDay = GetTimeOfDay(source.Providers.Select(p => p.DateTime).Distinct().Single()),
                        Cloudy = GetCloudy(source.Cloudy),
                        Precipitation = GetPrecipitation(source.Precipitation, source.StrengthPrecipitation, source.IsFog, source.IsThunderstorm),
                        AirTemp = source.AirTemp,
                        RealFeel = source.RealFeel,
                        Pressure = source.Pressure,
                        WindDirection = GetWindDirection(source.WindDirection),
                        WindSpeed = source.WindSpeed,
                        Humidity = source.Humidity,
                        ChancePrecipitation = source.ChancePrecipitation
                    });
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
            sb.Append(GetPrecipitation(precipitation, strengthPrecipitation, sb.Length == 0));
            sb.AppendFormat(GetThunderstorm(isThunderstorm, sb.Length == 0));
            sb.Append(isFog ? GetEditedMessage(AppResource.WeatherData_Fog, sb.Length == 0) : string.Empty);
            
            return sb.ToString();
        }

        private static string GetPrecipitation(PrecipitationType precipitation, StrengthPrecipitationType strengthPrecipitation, bool isFirst)
        {
            var result = GetStrengthPrecipitation(strengthPrecipitation, isFirst);

            switch (precipitation)
            {
                case PrecipitationType.Rain:
                    return string.IsNullOrEmpty(result) ? AppResource.PrecipitationType_Rain : " {0}".F(AppResource.PrecipitationType_Rain.ToLower());

                case PrecipitationType.Sleet:
                    return string.IsNullOrEmpty(result) ? AppResource.PrecipitationType_Sleet : " {0}".F(AppResource.PrecipitationType_Sleet.ToLower());

                case PrecipitationType.Snow:
                    return string.IsNullOrEmpty(result) ? AppResource.PrecipitationType_Snow : " {0}".F(AppResource.PrecipitationType_Snow.ToLower());
            }

            return string.Empty;
        }

        private static string GetStrengthPrecipitation(StrengthPrecipitationType strengthPrecipitation, bool isFirst)
        {
            switch (strengthPrecipitation)
            {
                case StrengthPrecipitationType.Light:
                    return GetEditedMessage(AppResource.StrengthPrecipitationType_Light, isFirst);

                case StrengthPrecipitationType.Heavy:
                    return GetEditedMessage(AppResource.StrengthPrecipitationType_Heavy, isFirst);
            }

            return string.Empty;
        }

        private static string GetThunderstorm(bool isThunderstorm, bool isFirst)
        {
            if (isThunderstorm)
            {
                return GetEditedMessage(AppResource.WeatherData_Thunderstorm, isFirst);
            }
            
            return string.Empty;
        }

        private static string GetEditedMessage(string message, bool isFirst)
        {
            return isFirst ? message : ", {0}".F(message.ToLower());
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