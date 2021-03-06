﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

using HtmlAgilityPack;

using Weather.Common.Dto;
using Weather.Common.Enums;
using Weather.Common.Exceptions;
using Weather.Parser.Extensions;
using Weather.Utilities.Extensions;

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
            var cloudy = GetXPath(htmlDocument, "Облачность", "/td[{0}]/div[1]/div".F(parseInfo.TimeOfDay));

            // In one time of day two description
            var description = GetXPath(htmlDocument, "Явления погоды", "/td[{0}]/div[1]".F(parseInfo.TimeOfDay * 2 - 2));
            var isFog = "//*[@id='forecastTable']/tr[5]/td[{0}]/div[1]".F(parseInfo.TimeOfDay - 1);
            var airTemp = GetXPath(htmlDocument, "Температура", "/td[{0}]/div[1]/b".F(parseInfo.TimeOfDay));
            var realFeel = GetXPath(htmlDocument, "Ощущается", "/td[{0}]/div[1]".F(parseInfo.TimeOfDay));
            var sameRealFeel = GetXPath(htmlDocument, "Ощущается", "/td[{0}]".F(parseInfo.TimeOfDay));
            var pressure = "//*[@id='forecastTable']/tr[last()-7]/td[{0}]/div[1]/b|//*[@id='forecastTable']/tr[last()-7]/td[{0}]/div[1]".F(parseInfo.TimeOfDay);
            var windDirection = "//*[@id='forecastTable']/tr[last()-4]/td[{0}]".F(parseInfo.TimeOfDay);
            var windSpeed = GetAvailablePath(htmlDocument,
                "//*[@id='forecastTable']/tr[last()-6]/td[{0}]/div[1]".F(parseInfo.TimeOfDay),
                "//*[@id='forecastTable']/tr[last()-6]/td[{0}]".F(parseInfo.TimeOfDay));
            var humidity = "//*[@id='forecastTable']/tr[last()-3]/td[{0}]".F(parseInfo.TimeOfDay);
            var date = "//*[@id='forecastTable']/tr[1]/td[{0}]/div/div/span[2]".F(parseInfo.Day);
            
            return new WeatherDataDto
            {
                Provider = Provider.Rp5,
                ProviderName = "Rp5",
                DateTime = this.GetDateInTable(htmlDocument.GetInnerText(date), parseInfo.TimeOfDayKey),
                Cloudy = this.ConvertCloudy(htmlDocument.GetAttribute(cloudy, "onmouseover")),
                Precipitation = this.ConvertPrecipitation(htmlDocument.GetAttribute(description, "onmouseover")),
                StrengthPrecipitation = this.ConvertStrengthPrecipitation(htmlDocument.GetAttribute(description, "onmouseover")),
                IsFog = this.ConvertFog(htmlDocument.IsAttribute(isFog, "onmouseover") ? htmlDocument.GetAttribute(isFog, "onmouseover") : string.Empty),
                IsThunderstorm = this.ConvertThunderstorm(htmlDocument.GetAttribute(description, "onmouseover")),
                AirTemp = Int32.Parse(htmlDocument.GetInnerText(airTemp)),
                RealFeel = this.ConvertRealFeel(htmlDocument, sameRealFeel, airTemp, realFeel),
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
            North = @"^С$";
            NorthEast = @"С-В";
            East = @"^В$";
            SouthEast = @"Ю-В";
            South = @"^Ю$";
            SouthWest = @"Ю-З";
            West = @"^З$";
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

            if (avaibleXPath != null)
                return avaibleXPath;

            throw new HtmlDocumentNotExistXPathException(string.Join(" | ", args));
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

        private string GetXPath(HtmlDocument htmlDocument, string rowPattern, string pattern)
        {
            var result = htmlDocument.DocumentNode.SelectNodes("//*[@id='forecastTable']/tr")
                .FirstOrDefault(i => Regex.IsMatch(i.InnerHtml, rowPattern));

            return result == null ? null : result.XPath + pattern;
        }

        #region Converters

        private double ConvertRealFeel(HtmlDocument htmlDocument, string sameRealFeelXPath, string airTempXPath, string realFeelXPath)
        {
            if (!string.IsNullOrEmpty(sameRealFeelXPath) && !string.IsNullOrEmpty(realFeelXPath) && !IsSameRealFeel(htmlDocument, sameRealFeelXPath))
            {
                return double.Parse(htmlDocument.GetInnerText(realFeelXPath));
            }

            return double.Parse(htmlDocument.GetInnerText(airTempXPath));
        }

        private bool IsSameRealFeel(HtmlDocument htmlDocument, string sameRealFeelXPath)
        {
            var isSameRealFeel = htmlDocument.IsAttribute(sameRealFeelXPath, "onmouseover");

            return isSameRealFeel && IsMatch(htmlDocument.GetAttribute(sameRealFeelXPath, "onmouseover"), "равно значению температуры воздуха");
        }

        #endregion
    }
}
