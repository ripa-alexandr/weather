
using System;

using Weather.Common.Enums;
using Weather.Common.Interfaces;

namespace Weather.Common.Entities
{
    public class WeatherDataEntity : IBaseEntity
    {
        public int Id { get; set; }

        public Provider Provider { get; set; }

        public string ProviderName { get; set; }

        public DateTime DateTime { get; set; }

        public CloudyType Cloudy { get; set; }

        public Precipitation? Precipitation { get; set; }

        public StrengthPrecipitation? StrengthPrecipitation { get; set; }

        public bool IsFog { get; set; }

        public bool IsThunderstorm { get; set; }

        public double AirTemp { get; set; }

        public double? RealFeel { get; set; }

        public double Pressure { get; set; }

        public WindDirection? WindDirection { get; set; }

        public double WindSpeed { get; set; }

        public double Humidity { get; set; }

        public double? ChancePrecipitation { get; set; }

        public int CityId { get; set; }

        public virtual CityEntity City { get; set; }
    }
}
