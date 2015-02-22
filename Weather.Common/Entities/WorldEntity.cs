
using System.Collections.Generic;

namespace Weather.Common.Entities
{
    public class WorldEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CountryEntity> Countries { get; set; }
    }
}
