
using System.Collections.Generic;

using Weather.Common.Interfaces;

namespace Weather.DAL.Entities
{
    public class WorldEntity : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CountryEntity> Countries { get; set; }
    }
}
