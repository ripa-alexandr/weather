
using System.Collections.Generic;

using Weather.Common.Interfaces;

namespace Weather.Common.Entities
{
    public class RegionEntity : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual CountryEntity Country { get; set; }

        public virtual ICollection<CityEntity> Cities { get; set; }
    }
}
