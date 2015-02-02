
using System.Collections.Generic;

using Weather.Common.Entities;

namespace Weather.Parser
{
    public interface IWeatherProvider
    {
        /// <summary>
        /// Parse web page and get data from this one
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        IEnumerable<WeatherData> Fetch(string url);
    }
}
