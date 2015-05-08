
using System.Collections.Generic;

namespace Weather.Common.Dto
{
    public class CityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RegionId { get; set; }

        public RegionDto Region { get; set; }

        public IEnumerable<WeatherDataDto> WeatherData { get; set; }

        public IEnumerable<LinkDto> Links { get; set; }
    }
}
