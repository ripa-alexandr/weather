
using System;
using System.Text.RegularExpressions;

using HtmlAgilityPack;

using Weather.Common.Entities;
using Weather.Common.Enums;
using Weather.Common.Exceptions;
using Weather.Common.Message.Request;
using Weather.Common.Message.Response;

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

        public virtual ProviderResponse Fetch(ProviderRequest request)
        {
            return null;
        }

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

        protected virtual TypeCloudy ConvertCloudy(string input)
        {
            if (Regex.IsMatch(input, Fair))
                return TypeCloudy.Fair;

            if (Regex.IsMatch(input, PartlyCloudy))
                return TypeCloudy.PartlyCloudy;

            if (Regex.IsMatch(input, Cloudy))
                return TypeCloudy.Cloudy;

            if (Regex.IsMatch(input, MainlyCloudy))
                return TypeCloudy.MainlyCloudy;

            if (Regex.IsMatch(input, Overcast) || this.ConvertFog(input))
                return TypeCloudy.Overcast;

            throw new NotImplementedMethodException(this.HtmlWeb.ResponseUri.ToString(), input);
        }

        protected virtual TypePrecipitation ConvertTypePrecipitation(string input)
        {
            if (Regex.IsMatch(input, Rain))
                return TypePrecipitation.Rain;

            if (Regex.IsMatch(input, Sleet))
                return TypePrecipitation.Sleet;

            if (Regex.IsMatch(input, Snow))
                return TypePrecipitation.Snow;

            return TypePrecipitation.None;
        }

        protected virtual TypeStrengthPrecipitation ConvertStrengthPrecipitation(string input)
        {
            if (this.ConvertTypePrecipitation(input) == TypePrecipitation.None)
                return TypeStrengthPrecipitation.None;

            if (Regex.IsMatch(input, Light))
                return TypeStrengthPrecipitation.Light;

            if (Regex.IsMatch(input, Heavy))
                return TypeStrengthPrecipitation.Heavy;

            return TypeStrengthPrecipitation.Middle;
        }

        protected virtual bool ConvertFog(string input)
        {
            return Regex.IsMatch(input, Fog);
        }

        protected virtual bool ConvertThunderstorm(string input)
        {
            return Regex.IsMatch(input, Thunderstorm);
        }

        protected virtual TypeWindDirection ConvertWindDirection(string input)
        {
            if (Regex.IsMatch(input, North))
                return TypeWindDirection.North;

            if (Regex.IsMatch(input, NorthEast))
                return TypeWindDirection.NorthEast;

            if (Regex.IsMatch(input, East))
                return TypeWindDirection.East;

            if (Regex.IsMatch(input, SouthEast))
                return TypeWindDirection.SouthEast;

            if (Regex.IsMatch(input, South))
                return TypeWindDirection.South;

            if (Regex.IsMatch(input, SouthWest))
                return TypeWindDirection.SouthWest;

            if (Regex.IsMatch(input, West))
                return TypeWindDirection.West;

            if (Regex.IsMatch(input, NorthWest))
                return TypeWindDirection.NorthWest;

            if (Regex.IsMatch(input, Calm))
                return TypeWindDirection.North;

            throw new NotImplementedMethodException(this.HtmlWeb.ResponseUri.ToString(), input);
        }

        #endregion
    }
}
