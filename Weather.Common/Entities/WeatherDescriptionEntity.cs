
namespace Weather.Common.Entities
{
    public class WeatherDescriptionEntity
    {
        public int Id { get; set; }

        public CloudyTypeEntity Cloudy { get; set; }

        public PrecipitationTypeEntity Precipitation { get; set; }

        public StrengthPrecipitationTypeEntity StrengthPrecipitation { get; set; }

        public bool IsFog { get; set; }

        public bool IsThunderstorm { get; set; }

        public double AirTemp { get; set; }

        public double? RealFeel { get; set; }

        public double Pressure { get; set; }

        public WindDirectionTypeEntity WindDirection { get; set; }

        public double WindSpeed { get; set; }

        public double Humidity { get; set; }

        public double? ChancePrecipitation { get; set; }

        public virtual WeatherDataEntity WeatherData { get; set; }
    }
}
