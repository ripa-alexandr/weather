
using System.Collections.Generic;

namespace Weather.Common.Entities
{
    public class CountryEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual WorldEntity World { get; set; }

        public virtual ICollection<RegionEntity> Regions { get; set; }
    }
}
