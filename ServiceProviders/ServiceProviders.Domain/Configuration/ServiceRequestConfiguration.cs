using ServiceProviders.Domain.DataServiceConfiguration;
using System;
using System.Net.Http;
using System.Text;

namespace ServiceProviders.Domain.Configuration
{
    public class ServiceRequestConfiguration : IServiceRequestConfiguration
    {
        private const string DEFAULT_MEDIA_TYPE = "application/json";

        #region public methods
        
        /// <inheritdoc/>
        public StringContent GetHttpRequestContent(string input)
        {
            return new StringContent(input, Encoding.UTF8, DEFAULT_MEDIA_TYPE);
        }

        /// <inheritdoc/>
        public HttpRequestMessage GetHttpRequestMessage(DataServiceSettings serviceSettings, string endPointKey, string[] mediaTypes = null)
        {
            var endPoint = serviceSettings.GetEndPoint(endPointKey);
            return GetHttpRequestMessageForEndPoint(endPoint, mediaTypes);
        }

        #endregion

        #region private methods
        private HttpRequestMessage GetHttpRequestMessageForEndPoint((HttpMethod RequestMethod, Uri RequestUri) endPoint, string[] mediaTypes = null)
        {
            var request = new HttpRequestMessage(endPoint.RequestMethod, endPoint.RequestUri);

            AddRequestHeaders(ref request, mediaTypes);

            return request;
        }

        private void AddRequestHeaders(ref HttpRequestMessage request, string[] mediaTypes = null)
        {
            if (mediaTypes != null)
            {
                request.Headers.Add("Accept", mediaTypes);
            }
            else
            {
                request.Headers.Add("Accept", DEFAULT_MEDIA_TYPE);
            }
        }
        #endregion
    }
}
