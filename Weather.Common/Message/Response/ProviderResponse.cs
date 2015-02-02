
using System.Collections.Generic;

using Weather.Common.Entities;

namespace Weather.Common.Message.Response
{
    public class ProviderResponse
    {
        public IEnumerable<WeatherData> WeatherData { get; set; }
    }
}
