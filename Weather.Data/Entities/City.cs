
using System.Collections.Generic;

using Weather.Data.Interfaces;

namespace Weather.Data.Entities
{
    public class City : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Region Region { get; set; }

        public virtual ICollection<WeatherData> WeatherData { get; set; }

        public virtual ICollection<Link> Links { get; set; }
    }
}
