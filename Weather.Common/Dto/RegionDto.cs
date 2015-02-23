
using System.Collections.Generic;

namespace Weather.Common.Dto
{
    public class RegionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CountryDto Country { get; set; }

        public IEnumerable<CityDto> Cities { get; set; }
    }
}
