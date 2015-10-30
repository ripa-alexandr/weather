
using System.Collections.Generic;

using Weather.Common.Interfaces;

namespace Weather.DAL.Entities
{
    public class RegionEntity : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual CountryEntity Country { get; set; }

        public virtual ICollection<CityEntity> Cities { get; set; }
    }
}
