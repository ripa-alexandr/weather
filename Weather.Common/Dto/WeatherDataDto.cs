
using System;

using Weather.Common.Enums;

namespace Weather.Common.Dto
{
    public class WeatherDataDto
    {
        public int Id { get; set; }

        public ProviderType Provider { get; set; }

        public string ProviderName { get; set; }

        public DateTime DateTime { get; set; }

        public CloudyType Cloudy { get; set; }

        public PrecipitationType Precipitation { get; set; }

        public StrengthPrecipitationType StrengthPrecipitation { get; set; }

        public bool IsFog { get; set; }

        public bool IsThunderstorm { get; set; }

        public double AirTemp { get; set; }

        public double? RealFeel { get; set; }

        public double Pressure { get; set; }

        public WindDirectionType WindDirection { get; set; }

        public double WindSpeed { get; set; }

        public double Humidity { get; set; }

        public double? ChancePrecipitation { get; set; }

        public int CityId { get; set; }

        public CityDto City { get; set; }
    }
}
