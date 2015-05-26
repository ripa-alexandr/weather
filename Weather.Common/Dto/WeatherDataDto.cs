
using Weather.Common.Enums;

namespace Weather.Common.Dto
{
    public class WeatherDataDto : WeatherDataBaseDto
    {
        public int Id { get; set; }

        public ProviderType Provider { get; set; }

        public string ProviderName { get; set; }

        public int CityId { get; set; }

        public CityDto City { get; set; }
    }
}
