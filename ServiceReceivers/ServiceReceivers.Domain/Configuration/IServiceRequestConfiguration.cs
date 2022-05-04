using ServiceReceivers.Domain.DataServiceConfiguration;
using System.Net.Http;

namespace ServiceReceivers.Domain.Configuration
{
    public interface IServiceRequestConfiguration
    {
        /// <summary>
        /// Get the http request content when other service is called
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        StringContent GetHttpRequestContent(string input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceSettings"></param>
        /// <param name="endPointKey"></param>
        /// <param name="mediaTypes"></param>
        /// <returns></returns>
        HttpRequestMessage GetHttpRequestMessage(DataServiceSettings serviceSettings, string endPointKey, string[] mediaTypes = null);

    }
}
