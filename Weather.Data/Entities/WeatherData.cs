
using System;

using Weather.Data.Interfaces;

namespace Weather.Data.Entities
{
    public class WeatherData : IBaseEntity
    {
        public int Id { get; set; }

        public TypeProvider TypeProvider { get; set; }

        public string NameProvider { get; set; }

        public DateTime DateTime { get; set; }

        public virtual WeatherDescription WeatherDescription { get; set; }

        public virtual City City { get; set; }
    }
}
