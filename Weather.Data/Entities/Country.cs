
using System.Collections.Generic;

using Weather.Data.Interfaces;

namespace Weather.Data.Entities
{
    public class Country : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual World World { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
