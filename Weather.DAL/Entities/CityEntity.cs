
using System.Collections.Generic;

using Weather.Common.Interfaces;

namespace Weather.DAL.Entities
{
    public class CityEntity : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RegionId { get; set; }

        public virtual RegionEntity Region { get; set; }

        public virtual ICollection<WeatherDataEntity> WeatherData { get; set; }

        public virtual ICollection<LinkEntity> Links { get; set; }
    }
}
