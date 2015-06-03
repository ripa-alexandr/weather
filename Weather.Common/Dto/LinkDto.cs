
using Weather.Common.Enums;

namespace Weather.Common.Dto
{
    public class LinkDto
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public Provider Provider { get; set; }

        public int CityId { get; set; }

        public CityDto City { get; set; }
    }
}
