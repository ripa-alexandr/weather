
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using HtmlAgilityPack;

using Weather.Common.Dto;
using Weather.Common.Enums;
using Weather.Common.Exceptions;
using Weather.Parser.Extensions;
using Weather.Utilities.Extensions;

namespace Weather.Parser
{
    public class GismeteoProvider : BaseProvider
    {
        public GismeteoProvider(HtmlWeb htmlWeb) 
            : base(htmlWeb)
        {
            this.InitializeRegularExpression();
        }

        public override IEnumerable<WeatherDataDto> Fetch(string url)
        {
            var result = new Collection<WeatherDataDto>();

            var parseInfo = this.InitializeParseInfo();

            foreach (var items in parseInfo.GroupBy(i => i.Url))
            {
                // Need initialize new url for other days
                var newUrl = this.BuildUrl(url, items.First().Url);
                var htmlDocument = this.HtmlWeb.Load(newUrl);

                foreach (var item in items)
                {
                    result.Add(this.Fetch(htmlDocument, item));
                }
            }

            return result;
        }

        protected WeatherDataDto Fetch(HtmlDocument htmlDocument, ParseInfo parseInfo)
        {
            var description = "//*[@id='tbwdaily{0}']/tr[{1}]/td[@class='cltext']".F(parseInfo.Day, parseInfo.TimeOfDay);
            var airTemp = "//*[@id='tbwdaily{0}']/tr[{1}]/td[@class='temp']/*[@class='value m_temp c']".F(parseInfo.Day, parseInfo.TimeOfDay);
            var realFeel = "//*[@id='tbwdaily{0}']/tr[{1}]/td[7]/*[@class='value m_temp c']".F(parseInfo.Day, parseInfo.TimeOfDay);
            var pressure = "//*[@id='tbwdaily{0}']/tr[{1}]/td/*[@class='value m_press torr']".F(parseInfo.Day, parseInfo.TimeOfDay);
            var windDirection = "//*[@id='tbwdaily{0}']/tr[{1}]/td/dl[@class='wind']/dt".F(parseInfo.Day, parseInfo.TimeOfDay);
            var windSpeed = "//*[@id='tbwdaily{0}']/tr[{1}]/td/dl/dd/*[@class='value m_wind ms']".F(parseInfo.Day, parseInfo.TimeOfDay);
            var humidity = "//*[@id='tbwdaily{0}']/tr[{1}]/td[6]".F(parseInfo.Day, parseInfo.TimeOfDay);
            var date = "//*[@id='tbwdaily{0}']/tr[1]".F(parseInfo.Day);

            return new WeatherDataDto
            {
                Provider = Provider.Gismeteo,
                ProviderName = "Gismeteo",
                DateTime = this.GetDate(htmlDocument.GetAttribute(date, "id"), parseInfo.TimeOfDayKey),
                Cloudy = this.ConvertCloudy(htmlDocument.GetInnerText(description)),
                Precipitation = this.ConvertPrecipitation(htmlDocument.GetInnerText(description)),
                StrengthPrecipitation = this.ConvertStrengthPrecipitation(htmlDocument.GetInnerText(description)),
                IsFog = this.ConvertFog(htmlDocument.GetInnerText(description)),
                IsThunderstorm = this.ConvertThunderstorm(htmlDocument.GetInnerText(description)),
                AirTemp = this.ConvertTemp(htmlDocument.GetInnerText(airTemp)),
                RealFeel = this.ConvertTemp(htmlDocument.GetInnerText(realFeel)),
                Pressure = Double.Parse(htmlDocument.GetInnerText(pressure)),
                WindDirection = this.ConvertWindDirection(htmlDocument.GetInnerText(windDirection)),
                WindSpeed = Double.Parse(htmlDocument.GetInnerText(windSpeed)),
                Humidity = Double.Parse(htmlDocument.GetInnerText(humidity))
            };
        }

        private void InitializeRegularExpression()
        {
            Fair = @"Ясно";
            PartlyCloudy = @"Малооблачно";
            MainlyCloudy = @"Облачно";
            Overcast = @"Пасмурно|снег|дождь";
            Rain = @"дождь(?!\sсо снегом)|ливень";
            Sleet = @"дождь со снегом";
            Snow = @"снег";
            Light = @"небольшой";
            Heavy = @"сильный";
            Fog = @"туман|дымка";
            Thunderstorm = @"гроз";
            North = @"^С$";
            NorthEast = @"СВ";
            East = @"^В$";
            SouthEast = @"ЮВ";
            South = @"^Ю$";
            SouthWest = @"ЮЗ";
            West = @"^З$";
            NorthWest = @"СЗ";
            Calm = @"^Ш$";
        }

        private IEnumerable<ParseInfo> InitializeParseInfo()
        {
            var parsInfo = new List<ParseInfo>();

            foreach (Day day in Enum.GetValues(typeof(Day)))
            {
                foreach (TimeOfDay timeOfDay in Enum.GetValues(typeof(TimeOfDay)))
                {
                    parsInfo.Add(new ParseInfo
                    {
                        DayKey = day,
                        Day = this.GetDay(day),
                        TimeOfDayKey = timeOfDay,
                        TimeOfDay = (int)timeOfDay,
                        Url = this.GetUrl(day)
                    });
                }
            }

            return parsInfo;
        }

        private int GetDay(Day day)
        {
            switch (day)
            {
                case Day.Day1: return 1;
                case Day.Day2: return 2;
                case Day.Day3: return 1;
                case Day.Day4: return 2;
                case Day.Day5: return 1;
                case Day.Day6: return 2;
                case Day.Day7: return 3;
            }

            throw new NotImplementedMethodException(this.HtmlWeb.ResponseUri.ToString(), day.ToString());
        }

        private string GetUrl(Day day)
        {
            switch (day)
            {
                case Day.Day1: return string.Empty;
                case Day.Day2: return string.Empty;
                case Day.Day3: return "3-5-days";
                case Day.Day4: return "3-5-days";
                case Day.Day5: return "5-7-days";
                case Day.Day6: return "5-7-days";
                case Day.Day7: return "5-7-days";
            }

            throw new NotImplementedMethodException(this.HtmlWeb.ResponseUri.ToString(), day.ToString());
        }

        #region Converters

        protected override CloudyType ConvertCloudy(string input)
        {
            if (IsMatch(input, Fair))
                return CloudyType.Fair;

            if (IsMatch(input, PartlyCloudy))
                return CloudyType.PartlyCloudy;

            if (IsMatch(input, MainlyCloudy))
                return CloudyType.MainlyCloudy;

            if (IsMatch(input, Overcast) || this.ConvertFog(input))
                return CloudyType.Overcast;

            throw new NotImplementedMethodException(this.HtmlWeb.ResponseUri.ToString(), input);
        }

        private double ConvertTemp(string input)
        {
            return Double.Parse(input.Replace("&minus;", "-"));
        }

        #endregion
    }
}
