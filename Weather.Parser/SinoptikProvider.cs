
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using HtmlAgilityPack;

using Weather.Common.Entities;
using Weather.Common.Enums;
using Weather.Parser.Extensions;

namespace Weather.Parser
{
    public class SinoptikProvider : BaseProvider
    {
        public SinoptikProvider(HtmlWeb htmlWeb)
            : base(htmlWeb)
        {
            this.InitializeRegularExpression();
        }

        public override IEnumerable<WeatherData> Fetch(string url)
        {
            var result = new Collection<WeatherData>();

            var parseInfo = this.InitializeParseInfo(url);
            
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
            var description = this.Format("//*[@id='bd{0}c']/div/div[2]/table/tbody/*[@class='img weatherIcoS']/td[{1}]/div", parseInfo.Day, parseInfo.TimeOfDay);
            var airTemp = this.Format("//*[@id='bd{0}c']/div/div[2]/table/tbody/*[@class='temperature']/td[{1}]", parseInfo.Day, parseInfo.TimeOfDay);
            var realFeel = this.Format("//*[@id='bd{0}c']/div/div[2]/table/tbody/*[@class='temperatureSens']/td[{1}]", parseInfo.Day, parseInfo.TimeOfDay);
            var pressure = this.Format("//*[@id='bd{0}c']/div/div[2]/table/tbody/*[@class='gray']/td[{1}]", parseInfo.Day, parseInfo.TimeOfDay);
            var windDirection = this.Format("//*[@id='bd{0}c']/div/div[2]/table/tbody/*[@class='gray']/td[{1}]/div", parseInfo.Day, parseInfo.TimeOfDay);
            var windSpeed = this.Format("//*[@id='bd{0}c']/div/div[2]/table/tbody/*[@class='gray']/td[{1}]/div", parseInfo.Day, parseInfo.TimeOfDay);
            var humidity = this.Format("//*[@id='bd{0}c']/div/div[2]/table/tbody/tr[6]/td[{1}]", parseInfo.Day, parseInfo.TimeOfDay);
            var chancePrecipitation = this.Format("//*[@id='bd{0}c']/div/div[2]/table/tbody/tr[8]/td[{1}]", parseInfo.Day, parseInfo.TimeOfDay);
            var date = string.Format("//*[@id='bd{0}']/p/a", parseInfo.Day);

            return new WeatherData
            {
                TypeProvider = TypeProvider.Sinoptik,
                NameProvider = "Sinoptic",
                DateTime = this.GetDate(htmlDocument.GetAttribute(date, "data-link"), parseInfo.TimeOfDayKey),
                WeatherDescription = new WeatherDescription
                {
                    Cloudy = this.ConvertCloudy(htmlDocument.GetAttribute(description, "title")),
                    TypePrecipitation = this.ConvertTypePrecipitation(htmlDocument.GetAttribute(description, "title")),
                    StrengthPrecipitation = this.ConvertStrengthPrecipitation(htmlDocument.GetAttribute(description, "title")),
                    IsFog = this.ConvertFog(htmlDocument.GetAttribute(description, "title")),
                    IsThunderstorm = this.ConvertThunderstorm(htmlDocument.GetAttribute(description, "title")),
                    AirTemp = this.ConvertTemp(htmlDocument.GetInnerText(airTemp)),
                    RealFeel = this.ConvertTemp(htmlDocument.GetInnerText(realFeel)),
                    Pressure = Double.Parse(htmlDocument.GetInnerText(pressure)),
                    WindDirection = this.ConvertWindDirection(htmlDocument.GetAttribute(windDirection, "data-tooltip")),
                    WindSpeed = this.ConvertWindSpeed(htmlDocument.GetInnerText(windSpeed)),
                    Humidity = Double.Parse(htmlDocument.GetInnerText(humidity)),
                    ChancePrecipitation = this.ConvertChancePrecipitation(htmlDocument.GetInnerText(chancePrecipitation))
                }
            };
        }

        private void InitializeRegularExpression()
        {
            Fair = @"Ясно";
            PartlyCloudy = @"Небольшая облачность";
            Cloudy = @"Переменная облачность";
            MainlyCloudy = @"Облачно с прояснениями";
            Overcast = @"Сплошная облачность";
            Rain = @"[Дд]ождь";
            Sleet = @"мокрый снег|дождь со снегом";
            Snow = @"[Сс]нег";
            Light = @"мелкий|небольшой";
            Heavy = @"сильный";
            Fog = @"[Тт]уман";
            Thunderstorm = @"гроз";
            North = @"Северный";
            NorthEast = @"Северо-восточный";
            East = @"Восточный";
            SouthEast = @"Юго-восточный";
            South = @"Южный.*";
            SouthWest = @"Юго-западный";
            West = @"Западный";
            NorthWest = @"Северо-западный";
            Calm = @"Штиль";
        }

        private IEnumerable<ParseInfo> InitializeParseInfo(string url)
        {
            var htmlDocument = this.HtmlWeb.Load(url);
            var date = this.GetDate(htmlDocument.GetAttribute("//*[@id='bd1']/p/a", "data-link"));
            var parsInfo = new List<ParseInfo>();

            foreach (Day day in Enum.GetValues(typeof(Day)))
            {
                foreach (TimeOfDay timeOfDay in Enum.GetValues(typeof(TimeOfDay)))
                {
                    parsInfo.Add(new ParseInfo
                    {
                        DayKey = day, 
                        Day = (int)day, 
                        TimeOfDayKey = timeOfDay, 
                        TimeOfDay = (int)timeOfDay, 
                        Url = this.GetUrl(date, (int)day - 1)
                    });
                }
            }

            return parsInfo;
        }

        private string GetUrl(DateTime date, int days)
        {
            return days == 0 ? string.Empty : date.AddDays(days).ToString("yyyy-MM-dd");
        }

        private string Format(string str, int day, int timeOfDay)
        {
            return string.Format(str, day, day == 1 || day == 2 ? timeOfDay * 2 : timeOfDay);
        }

        #region Converters

        private double ConvertTemp(string input)
        {
            return Double.Parse(input.Replace("&deg;", string.Empty));
        }

        private double ConvertWindSpeed(string input)
        {
            return Double.Parse(input.Replace('.', ','));
        }

        private double ConvertChancePrecipitation(string input)
        {
            return Double.Parse(input.Replace('-', '0'));
        }

        #endregion
    }
}
