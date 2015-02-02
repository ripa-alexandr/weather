
using Weather.Common.Message.Request;
using Weather.Common.Message.Response;

namespace Weather.Parser
{
    public interface IWeatherProvider
    {
        /// <summary>
        /// Get weather data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ProviderResponse Fetch(ProviderRequest request);
    }
}
