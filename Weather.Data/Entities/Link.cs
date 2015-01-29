
using Weather.Data.Interfaces;

namespace Weather.Data.Entities
{
    public class Link : IBaseEntity
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public TypeProvider TypeProvider { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
