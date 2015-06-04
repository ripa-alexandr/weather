
using System;
using System.Linq;
using System.Text;

using AutoMapper;

using Weather.Bootstrap;
using Weather.Common.Dto;
using Weather.Common.Enums;
using Weather.Utilities.Extensions;
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

        private static string GetPrecipitation(Precipitation? precipitation, StrengthPrecipitation? strengthPrecipitation, bool isFog, bool isThunderstorm)
        {
            var sb = new StringBuilder();
            sb.Append(GetPrecipitation(precipitation, strengthPrecipitation, sb.Length == 0));
            sb.AppendFormat(GetThunderstorm(isThunderstorm, sb.Length == 0));
            sb.Append(isFog ? GetEditedMessage(AppResource.WeatherData_Fog, sb.Length == 0) : string.Empty);
            
            return sb.ToString();
        }

        private static string GetPrecipitation(Precipitation? precipitation, StrengthPrecipitation? strengthPrecipitation, bool isFirst)
        {
            if (!precipitation.HasValue) 
                return string.Empty;

            var result = GetStrengthPrecipitation(strengthPrecipitation, isFirst);

            switch (precipitation)
            {
                case Precipitation.Rain:
                    return string.IsNullOrEmpty(result) ? AppResource.Precipitation_Rain : " {0}".F(AppResource.Precipitation_Rain.ToLower());

                case Precipitation.Sleet:
                    return string.IsNullOrEmpty(result) ? AppResource.Precipitation_Sleet : " {0}".F(AppResource.Precipitation_Sleet.ToLower());

                case Precipitation.Snow:
                    return string.IsNullOrEmpty(result) ? AppResource.Precipitation_Snow : " {0}".F(AppResource.Precipitation_Snow.ToLower());
            }

            return string.Empty;
        }

        private static string GetStrengthPrecipitation(StrengthPrecipitation? strengthPrecipitation, bool isFirst)
        {
            if (!strengthPrecipitation.HasValue)
                return string.Empty;

            switch (strengthPrecipitation)
            {
                case StrengthPrecipitation.Light:
                    return GetEditedMessage(AppResource.StrengthPrecipitation_Light, isFirst);

                case StrengthPrecipitation.Heavy:
                    return GetEditedMessage(AppResource.StrengthPrecipitation_Heavy, isFirst);
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

        private static string GetWindDirection(WindDirection? direction)
        {
            switch (direction)
            {
                case null:
                    return AppResource.WindDirection_Calm;

                case WindDirection.North:
                    return AppResource.WindDirection_North;

                case WindDirection.NorthEast:
                    return AppResource.WindDirection_NorthEast;

                case WindDirection.East:
                    return AppResource.WindDirection_East;

                case WindDirection.SouthEast:
                    return AppResource.WindDirection_SouthEast;

                case WindDirection.South:
                    return AppResource.WindDirection_South;

                case WindDirection.SouthWest:
                    return AppResource.WindDirection_SouthWest;

                case WindDirection.West:
                    return AppResource.WindDirection_West;

                case WindDirection.NorthWest:
                    return AppResource.WindDirection_NorthWest;
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