
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using HtmlAgilityPack;

using Weather.Common.Entities;
using Weather.Common.Enums;
using Weather.Common.Exceptions;

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

        public abstract IEnumerable<WeatherDataEntity> Fetch(string url);
        
        protected string BuildUrl(string oldUrl, string addUrl)
        {
            if (!string.IsNullOrWhiteSpace(addUrl))
            {
                return string.Format("{0}/{1}", oldUrl, addUrl);
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

        protected virtual CloudyTypeEntity ConvertCloudy(string input)
        {
            if (Regex.IsMatch(input, Fair))
                return CloudyTypeEntity.Fair;

            if (Regex.IsMatch(input, PartlyCloudy))
                return CloudyTypeEntity.PartlyCloudy;

            if (Regex.IsMatch(input, Cloudy))
                return CloudyTypeEntity.Cloudy;

            if (Regex.IsMatch(input, MainlyCloudy))
                return CloudyTypeEntity.MainlyCloudy;

            if (Regex.IsMatch(input, Overcast) || this.ConvertFog(input))
                return CloudyTypeEntity.Overcast;

            throw new NotImplementedMethodException(this.HtmlWeb.ResponseUri.ToString(), input);
        }

        protected virtual PrecipitationTypeEntity ConvertTypePrecipitation(string input)
        {
            if (Regex.IsMatch(input, Rain))
                return PrecipitationTypeEntity.Rain;

            if (Regex.IsMatch(input, Sleet))
                return PrecipitationTypeEntity.Sleet;

            if (Regex.IsMatch(input, Snow))
                return PrecipitationTypeEntity.Snow;

            return PrecipitationTypeEntity.None;
        }

        protected virtual StrengthPrecipitationTypeEntity ConvertStrengthPrecipitation(string input)
        {
            if (this.ConvertTypePrecipitation(input) == PrecipitationTypeEntity.None)
                return StrengthPrecipitationTypeEntity.None;

            if (Regex.IsMatch(input, Light))
                return StrengthPrecipitationTypeEntity.Light;

            if (Regex.IsMatch(input, Heavy))
                return StrengthPrecipitationTypeEntity.Heavy;

            return StrengthPrecipitationTypeEntity.Middle;
        }

        protected virtual bool ConvertFog(string input)
        {
            return Regex.IsMatch(input, Fog);
        }

        protected virtual bool ConvertThunderstorm(string input)
        {
            return Regex.IsMatch(input, Thunderstorm);
        }

        protected virtual WindDirectionTypeEntity ConvertWindDirection(string input)
        {
            if (Regex.IsMatch(input, North))
                return WindDirectionTypeEntity.North;

            if (Regex.IsMatch(input, NorthEast))
                return WindDirectionTypeEntity.NorthEast;

            if (Regex.IsMatch(input, East))
                return WindDirectionTypeEntity.East;

            if (Regex.IsMatch(input, SouthEast))
                return WindDirectionTypeEntity.SouthEast;

            if (Regex.IsMatch(input, South))
                return WindDirectionTypeEntity.South;

            if (Regex.IsMatch(input, SouthWest))
                return WindDirectionTypeEntity.SouthWest;

            if (Regex.IsMatch(input, West))
                return WindDirectionTypeEntity.West;

            if (Regex.IsMatch(input, NorthWest))
                return WindDirectionTypeEntity.NorthWest;

            if (Regex.IsMatch(input, Calm))
                return WindDirectionTypeEntity.North;

            throw new NotImplementedMethodException(this.HtmlWeb.ResponseUri.ToString(), input);
        }

        #endregion
    }
}
