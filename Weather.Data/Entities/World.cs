
using System.Collections.Generic;

using Weather.Data.Interfaces;

namespace Weather.Data.Entities
{
    public class World : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
