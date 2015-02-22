
using System.Collections.Generic;

namespace Weather.Common.Entities
{
    public class CityEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual RegionEntity Region { get; set; }

        public virtual ICollection<WeatherDataEntity> WeatherData { get; set; }

        public virtual ICollection<LinkEntity> Links { get; set; }
    }
}
