
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

using HtmlAgilityPack;

using Weather.Data.Entities;
using Weather.Data.Enums;
using Weather.Data.Exceptions;
using Weather.Parser.Extensions;

namespace Weather.Parser
{
    public class GismeteoProvider : BaseProvider
    {
        public GismeteoProvider(HtmlWeb htmlWeb) 
            : base(htmlWeb)
        {
            this.InitializeRegularExpression();
        }

        public override IEnumerable<WeatherData> Fetch(string url)
        {
            var result = new Collection<WeatherData>();

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

        protected WeatherData Fetch(HtmlDocument htmlDocument, ParseInfo parseInfo)
        {
            var description = this.Format("//*[@id='tbwdaily{0}']/tr[{1}]/td[@class='cltext']", parseInfo.Day, parseInfo.TimeOfDay);
            var airTemp = this.Format("//*[@id='tbwdaily{0}']/tr[{1}]/td[@class='temp']/*[@class='value m_temp c']", parseInfo.Day, parseInfo.TimeOfDay);
            var realFeel = this.Format("//*[@id='tbwdaily{0}']/tr[{1}]/td[7]/*[@class='value m_temp c']", parseInfo.Day, parseInfo.TimeOfDay);
            var pressure = this.Format("//*[@id='tbwdaily{0}']/tr[{1}]/td/*[@class='value m_press torr']", parseInfo.Day, parseInfo.TimeOfDay);
            var windDirection = this.Format("//*[@id='tbwdaily{0}']/tr[{1}]/td/dl[@class='wind']/dt", parseInfo.Day, parseInfo.TimeOfDay);
            var windSpeed = this.Format("//*[@id='tbwdaily{0}']/tr[{1}]/td/dl/dd/*[@class='value m_wind ms']", parseInfo.Day, parseInfo.TimeOfDay);
            var humidity = this.Format("//*[@id='tbwdaily{0}']/tr[{1}]/td[6]", parseInfo.Day, parseInfo.TimeOfDay);
            var date = string.Format("//*[@id='tbwdaily{0}']/tr[1]", parseInfo.Day);

            return new WeatherData
            {
                TypeProvider = TypeProvider.Gismeteo,
                NameProvider = "Gismeteo",
                DateTime = this.GetDate(htmlDocument.GetAttribute(date, "id"), parseInfo.TimeOfDayKey),
                WeatherDescription = new WeatherDescription
                {
                    Cloudy = this.ConvertCloudy(htmlDocument.GetInnerText(description)),
                    TypePrecipitation = this.ConvertTypePrecipitation(htmlDocument.GetInnerText(description)),
                    StrengthPrecipitation = this.ConvertStrengthPrecipitation(htmlDocument.GetInnerText(description)),
                    IsFog = this.ConvertFog(htmlDocument.GetInnerText(description)),
                    IsThunderstorm = this.ConvertThunderstorm(htmlDocument.GetInnerText(description)),
                    AirTemp = this.ConvertTemp(htmlDocument.GetInnerText(airTemp)),
                    RealFeel = this.ConvertTemp(htmlDocument.GetInnerText(realFeel)),
                    Pressure = Double.Parse(htmlDocument.GetInnerText(pressure)),
                    WindDirection = this.ConvertWindDirection(htmlDocument.GetInnerText(windDirection)),
                    WindSpeed = Double.Parse(htmlDocument.GetInnerText(windSpeed)),
                    Humidity = Double.Parse(htmlDocument.GetInnerText(humidity))
                }
            };
        }

        private void InitializeRegularExpression()
        {
            Fair = @"Ясно";
            PartlyCloudy = @"Малооблачно";
            MainlyCloudy = @"Облачно";
            Overcast = @"Пасмурно|[Сс]нег|[Дд]ождь";
            Rain = @"[Дд]ождь";
            Sleet = @"дождь со снегом";
            Snow = @"[Сс]нег";
            Light = @"небольшой";
            Heavy = @"сильный";
            Fog = @"[Тт]уман|^$|[Дд]ымка";
            Thunderstorm = @"гроз";
            North = @"\bС\b";
            NorthEast = @"СВ";
            East = @"\bВ\b";
            SouthEast = @"ЮВ";
            South = @"\bЮ\b";
            SouthWest = @"ЮЗ";
            West = @"\bЗ\b";
            NorthWest = @"СЗ";
            Calm = @"\bШ\b";
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

        private string Format(string str, int day, int timeOfDay)
        {
            return string.Format(str, day, timeOfDay);
        }

        #region Converters

        protected override TypeCloudy ConvertCloudy(string input)
        {
            if (Regex.IsMatch(input, Fair))
                return TypeCloudy.Fair;

            if (Regex.IsMatch(input, PartlyCloudy))
                return TypeCloudy.PartlyCloudy;

            if (Regex.IsMatch(input, MainlyCloudy))
                return TypeCloudy.MainlyCloudy;

            if (Regex.IsMatch(input, Overcast) || this.ConvertFog(input))
                return TypeCloudy.Overcast;

            throw new NotImplementedMethodException(this.HtmlWeb.ResponseUri.ToString(), input);
        }

        private double ConvertTemp(string input)
        {
            return Double.Parse(input.Replace("&minus;", "-"));
        }

        #endregion
    }
}
