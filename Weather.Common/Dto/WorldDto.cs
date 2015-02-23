
using System.Collections.Generic;

namespace Weather.Common.Dto
{
    public class WorldDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CountryDto> Countries { get; set; }
    }
}
