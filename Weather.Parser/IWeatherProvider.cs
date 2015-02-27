
using System.Collections.Generic;

using Weather.Common.Dto;

namespace Weather.Parser
{
    public interface IWeatherProvider
    {
        /// <summary>
        /// Parse web page and get data from this one
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        IEnumerable<WeatherDataDto> Fetch(string url);
    }
}
