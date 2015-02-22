
using System.Collections.Generic;

namespace Weather.Common.Entities
{
    public class RegionEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual CountryEntity Country { get; set; }

        public virtual ICollection<CityEntity> Cities { get; set; }
    }
}
