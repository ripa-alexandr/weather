
namespace Weather.Common.Entities
{
    public class LinkEntity
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public ProviderTypeEntity Provider { get; set; }

        public int CityId { get; set; }

        public virtual CityEntity City { get; set; }
    }
}
