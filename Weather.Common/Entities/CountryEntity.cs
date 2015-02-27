
using System.Collections.Generic;

using Weather.Common.Interfaces;

namespace Weather.Common.Entities
{
    public class CountryEntity : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual WorldEntity World { get; set; }

        public virtual ICollection<RegionEntity> Regions { get; set; }
    }
}
