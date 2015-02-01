
using System.Collections.Generic;

using Weather.Common.Interfaces;

namespace Weather.Common.Entities
{
    public class Region : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
