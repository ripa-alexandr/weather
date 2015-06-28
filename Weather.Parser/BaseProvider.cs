
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using HtmlAgilityPack;

using Weather.Common.Dto;
using Weather.Common.Enums;
using Weather.Common.Exceptions;
using Weather.Utilities;

namespace Weather.Parser
{
    public abstract class BaseProvider : IWeatherProvider
    {
        protected readonly HtmlWeb HtmlWeb;

        protected string Fair;
        protected string PartlyCloudy;
        protected string Cloudy;
        protected string MainlyCloudy;
        protected string Overcast;
        protected string Rain;
        protected string Sleet;
        protected string Snow;
        protected string Light;
        protected string Heavy;
        protected string Fog;
        protected string Thunderstorm;
        protected string North;
        protected string NorthEast;
        protected string East;
        protected string SouthEast;
        protected string South;
        protected string SouthWest;
        protected string West;
        protected string NorthWest;
        protected string Calm;
        
        protected BaseProvider(HtmlWeb htmlWeb)
        {
            this.HtmlWeb = htmlWeb;
        }

        public abstract IEnumerable<WeatherDataDto> Fetch(string url);
        
        protected string BuildUrl(string oldUrl, string addUrl)
        {
            if (!string.IsNullOrWhiteSpace(addUrl))
            {
                return UrlUtility.Combine(oldUrl, addUrl);
            }

            return oldUrl;
        }

        #region Date

        protected DateTime GetDate(string input)
        {
            var date = Regex.Match(input, @"\d{4}-\d{2}-\d{2}").ToString();

            return DateTime.Parse(date);
        }

        protected DateTime GetDate(string input, TimeOfDay timeOfDay)
        {
            var date = this.GetDate(input);

            return this.AddHours(date, timeOfDay);
        }

        protected DateTime AddHours(DateTime datetime, TimeOfDay timeOfDay)
        {
            switch (timeOfDay)
            {
                case TimeOfDay.Night:
                    return datetime.AddHours(3);

                case TimeOfDay.Morning:
                    return datetime.AddHours(9);

                case TimeOfDay.Midday:
                    return datetime.AddHours(15);

                case TimeOfDay.Evening:
                    return datetime.AddHours(21);
            }

            throw new NotImplementedMethodException(datetime.ToString("dd-MMM-yyyy"), timeOfDay.ToString());
        }

        #endregion

        #region Converters

        protected virtual CloudyType ConvertCloudy(string input)
        {
            if (IsMatch(input, Fair))
                return CloudyType.Fair;

            if (IsMatch(input, PartlyCloudy))
                return CloudyType.PartlyCloudy;

            if (IsMatch(input, Cloudy))
                return CloudyType.Cloudy;

            if (IsMatch(input, MainlyCloudy))
                return CloudyType.MainlyCloudy;

            if (IsMatch(input, Overcast) || this.ConvertFog(input))
                return CloudyType.Overcast;

            throw new NotImplementedMethodException(this.HtmlWeb.ResponseUri.ToString(), input);
        }

        protected virtual Precipitation? ConvertPrecipitation(string input)
        {
            if (IsMatch(input, Rain))
                return Precipitation.Rain;

            if (IsMatch(input, Sleet))
                return Precipitation.Sleet;

            if (IsMatch(input, Snow))
                return Precipitation.Snow;

            return null;
        }

        protected virtual StrengthPrecipitation? ConvertStrengthPrecipitation(string input)
        {
            if (!this.ConvertPrecipitation(input).HasValue)
                return null;

            if (IsMatch(input, Light))
                return StrengthPrecipitation.Light;

            if (IsMatch(input, Heavy))
                return StrengthPrecipitation.Heavy;

            return StrengthPrecipitation.Middle;
        }

        protected virtual bool ConvertFog(string input)
        {
            return IsMatch(input, Fog);
        }

        protected virtual bool ConvertThunderstorm(string input)
        {
            return IsMatch(input, Thunderstorm);
        }

        protected virtual WindDirection? ConvertWindDirection(string input)
        {
            if (IsMatch(input, North))
                return WindDirection.North;

            if (IsMatch(input, NorthEast))
                return WindDirection.NorthEast;

            if (IsMatch(input, East))
                return WindDirection.East;

            if (IsMatch(input, SouthEast))
                return WindDirection.SouthEast;

            if (IsMatch(input, South))
                return WindDirection.South;

            if (IsMatch(input, SouthWest))
                return WindDirection.SouthWest;

            if (IsMatch(input, West))
                return WindDirection.West;

            if (IsMatch(input, NorthWest))
                return WindDirection.NorthWest;

            if (IsMatch(input, Calm))
                return null;

            throw new NotImplementedMethodException(this.HtmlWeb.ResponseUri.ToString(), input);
        }

        protected bool IsMatch(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        #endregion
    }
}
