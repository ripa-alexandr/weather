
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

using HtmlAgilityPack;

using Weather.Common.Dto;
using Weather.Common.Enums;
using Weather.Common.Exceptions;
using Weather.Common.Extensions;
using Weather.Parser.Extensions;

namespace Weather.Parser
{
    public class Rp5Provider : BaseProvider
    {
        public Rp5Provider(HtmlWeb htmlWeb)
            : base(htmlWeb)
        {
            this.InitializeRegularExpression();
        }

        public override IEnumerable<WeatherDataDto> Fetch(string url)
        {
            var htmlDocument = this.HtmlWeb.Load(url);
            var result = new Collection<WeatherDataDto>();

            var parseInfo = this.InitializeParseInfo(htmlDocument);

            foreach (var item in parseInfo)
            {
                result.Add(this.Fetch(htmlDocument, item));
            }

            return result;
        }

        protected WeatherDataDto Fetch(HtmlDocument htmlDocument, ParseInfo parseInfo)
        {
            var cloudy = "//*[@id='forecastTable']/tr[3]/td[{0}]/div[1]/div".F(parseInfo.TimeOfDay);

            // In one time of day two description
            var description = "//*[@id='forecastTable']/tr[4]/td[{0}]/div[1]/div".F(parseInfo.TimeOfDay * 2 - 2);
            var airTemp = "//*[@id='forecastTable']/tr[5]/td[{0}]/div[1]/b".F(parseInfo.TimeOfDay);
            var realFeel = "//*[@id='forecastTable']/tr[6]/td[{0}]/div[1]".F(parseInfo.TimeOfDay);
            var isRealFeel = "//*[@id='forecastTable']/tr[6]/td[{0}]".F(parseInfo.TimeOfDay);
            var pressure = this.GetAvailablePath(
                htmlDocument,
                "//*[@id='forecastTable']/tr[7]/td[{0}]/div[1]/b".F(parseInfo.TimeOfDay),
                "//*[@id='forecastTable']/tr[7]/td[{0}]/div[1]".F(parseInfo.TimeOfDay));
            var windDirection = "//*[@id='forecastTable']/tr[10]/td[{0}]".F(parseInfo.TimeOfDay);
            var windSpeed = this.GetAvailablePath(
                htmlDocument, 
                "//*[@id='forecastTable']/tr[9]/td[{0}]/div[1]".F(parseInfo.TimeOfDay), 
                "//*[@id='forecastTable']/tr[9]/td[{0}]".F(parseInfo.TimeOfDay));
            var humidity = "//*[@id='forecastTable']/tr[8]/td[{0}]".F(parseInfo.TimeOfDay);
            var date = "//*[@id='forecastTable']/tr[1]/td[{0}]/div/div/span[2]".F(parseInfo.Day);

            return new WeatherDataDto
            {
                Provider = ProviderType.Rp5,
                ProviderName = "Rp5",
                DateTime = this.GetDateInTable(htmlDocument.GetInnerText(date), parseInfo.TimeOfDayKey),
                Cloudy = this.ConvertCloudy(htmlDocument.GetAttribute(cloudy, "onmouseover")),
                Precipitation = this.ConvertTypePrecipitation(htmlDocument.GetAttribute(description, "onmouseover")),
                StrengthPrecipitation = this.ConvertStrengthPrecipitation(htmlDocument.GetAttribute(description, "onmouseover")),
                IsFog = this.ConvertFog(htmlDocument.GetAttribute(description, "onmouseover")),
                IsThunderstorm = this.ConvertThunderstorm(htmlDocument.GetAttribute(description, "onmouseover")),
                AirTemp = Int32.Parse(htmlDocument.GetInnerText(airTemp)),
                RealFeel = this.ConvertRealFeel(htmlDocument, isRealFeel, airTemp, realFeel),
                Pressure = Double.Parse(htmlDocument.GetInnerText(pressure)),
                WindDirection = this.ConvertWindDirection(htmlDocument.GetInnerText(windDirection)),
                WindSpeed = Double.Parse(htmlDocument.GetInnerText(windSpeed)),
                Humidity = Double.Parse(htmlDocument.GetInnerText(humidity))
            };
        }

        private void InitializeRegularExpression()
        {
            Fair = @"ясно";
            PartlyCloudy = @"малооблачно|небольшая облачность|переменная облачность";
            Cloudy = @"облачно с прояснениями|облачно";
            MainlyCloudy = @"значительная облачность";
            Overcast = @"пасмурная погода";
            Rain = @"дождь(?!\sсо снегом)";
            Sleet = @"дождь со снегом";
            Snow = @"снег";
            Light = @"преимущественно без осадков";
            Heavy = @"сильный";
            Fog = @"туман";
            Thunderstorm = @"гроз";
            North = @"\bС\b";
            NorthEast = @"С-В";
            East = @"\bВ\b";
            SouthEast = @"Ю-В";
            South = @"\bЮ\b";
            SouthWest = @"Ю-З";
            West = @"\bЗ\b";
            NorthWest = @"С-З";
            Calm = @"ШТЛ";
        }

        private IEnumerable<ParseInfo> InitializeParseInfo(HtmlDocument htmlDocument)
        {
            var result = new List<ParseInfo>();

            var days = htmlDocument.DocumentNode.SelectNodes("//*[@id='forecastTable']/tr[1]/td[@colspan]").ToList();

            int counter = 1;

            for (int i = 1; i <= days.Count; i++)
            {
                var timesOfDay = Int32.Parse(days[i - 1].Attributes["colspan"].Value) / 2;

                for (int j = 1; j <= timesOfDay; j++)
                {
                    counter++;

                    result.Add(new ParseInfo
                    {
                        DayKey = (Day)i,
                        Day = i + 1,
                        TimeOfDayKey = i == days.Count ? (TimeOfDay)j : (TimeOfDay)4 - timesOfDay + j,
                        TimeOfDay = counter
                    });
                }
            }

            return result;
        }

        private string GetAvailablePath(HtmlDocument htmlDocument, params string[] args)
        {
            var avaibleXPath = args.FirstOrDefault(htmlDocument.IsInnerText);

            return avaibleXPath;
        }

        private DateTime GetDateInTable(string input, TimeOfDay timeOfDay)
        {
            var day = Regex.Match(input, @"\d{1,2}").ToString();
            var date = this.GetDateInTable(day);
            
            return this.GetDate(date, timeOfDay);
        }

        private string GetDateInTable(string day)
        {
            var date = DateTime.Now;

            for (int i = -1; i <= 7; i++)
            {
                if (date.AddDays(i).Day == Int32.Parse(day))
                {
                    return date.AddDays(i).ToString("yyyy-MM-dd");
                }
            }

            throw new NotImplementedMethodException(this.HtmlWeb.ResponseUri.ToString(), day);
        }

        #region Converters

        private double ConvertRealFeel(HtmlDocument htmlDocument, string isRealFeelXPath, string airTempXPath, string realFeelXPath)
        {
            var isRealFeel = htmlDocument.IsAttribute(isRealFeelXPath, "onmouseover");

            if (isRealFeel && Regex.IsMatch(htmlDocument.GetAttribute(isRealFeelXPath, "onmouseover"), "равно значению температуры воздуха"))
                return double.Parse(htmlDocument.GetInnerText(airTempXPath));

            return double.Parse(htmlDocument.GetInnerText(realFeelXPath));
        }

        #endregion
    }
}
