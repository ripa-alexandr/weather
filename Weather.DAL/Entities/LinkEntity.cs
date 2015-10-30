
using Weather.Common.Enums;
using Weather.Common.Interfaces;

namespace Weather.DAL.Entities
{
    public class LinkEntity : IBaseEntity
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public Provider Provider { get; set; }

        public int CityId { get; set; }

        public virtual CityEntity City { get; set; }
    }
}
