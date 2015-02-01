
using Weather.Common.Interfaces;

namespace Weather.Common.Entities
{
    public class WeatherDescription : IBaseEntity
    {
        public int Id { get; set; }

        public TypeCloudy Cloudy { get; set; }

        public TypePrecipitation TypePrecipitation { get; set; }

        public TypeStrengthPrecipitation StrengthPrecipitation { get; set; }

        public bool IsFog { get; set; }

        public bool IsThunderstorm { get; set; }

        public double AirTemp { get; set; }

        public double? RealFeel { get; set; }

        public double Pressure { get; set; }

        public TypeWindDirection WindDirection { get; set; }

        public double WindSpeed { get; set; }

        public double Humidity { get; set; }

        public double? ChancePrecipitation { get; set; }

        public virtual WeatherData WeatherData { get; set; }
    }
}
