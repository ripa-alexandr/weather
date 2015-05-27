
using System.Collections.Generic;

namespace Weather.Common.Dto
{
    public class WeatherDataAggregateDto : WeatherDataBaseDto
    {
        public IEnumerable<ProviderDto> Providers { get; set; }
    }
}
