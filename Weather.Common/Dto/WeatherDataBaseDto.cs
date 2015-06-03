using Weather.Common.Enums;

namespace Weather.Common.Dto
{
    public abstract class WeatherDataBaseDto
    {
        public CloudyType Cloudy { get; set; }

        public Precipitation Precipitation { get; set; }

        public StrengthPrecipitation StrengthPrecipitation { get; set; }

        public bool IsFog { get; set; }

        public bool IsThunderstorm { get; set; }

        public double AirTemp { get; set; }

        public double? RealFeel { get; set; }

        public double Pressure { get; set; }

        public WindDirection WindDirection { get; set; }

        public double WindSpeed { get; set; }

        public double Humidity { get; set; }

        public double? ChancePrecipitation { get; set; }
    }
}
