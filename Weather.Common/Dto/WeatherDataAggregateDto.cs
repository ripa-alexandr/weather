
using System.Collections.Generic;

using Weather.Common.Enums;

namespace Weather.Common.Dto
{
    public class WeatherDataAggregateDto : WeatherDataBaseDto
    {
        public IEnumerable<ProviderType> Providers { get; set; }
    }
}
