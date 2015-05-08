
using System.Collections.Generic;

namespace Weather.Common.Dto
{
    public class CountryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int WorldId { get; set; }

        public WorldDto World { get; set; }

        public IEnumerable<RegionDto> Regions { get; set; }
    }
}
