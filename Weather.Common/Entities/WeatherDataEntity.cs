
using System;

using Weather.Common.Enums;
using Weather.Common.Interfaces;

namespace Weather.Common.Entities
{
    public class WeatherDataEntity : IBaseEntity
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

        public virtual CityEntity City { get; set; }
    }
}
