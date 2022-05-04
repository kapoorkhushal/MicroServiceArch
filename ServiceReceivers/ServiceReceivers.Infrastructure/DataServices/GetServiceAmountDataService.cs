using Microsoft.Extensions.Options;
using ServiceReceivers.Application.Interfaces;
using ServiceReceivers.Domain.Configuration;
using ServiceReceivers.Domain.DataServiceConfiguration;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceReceivers.Infrastructure.DataService
{
    public class GetServiceAmountDataService : IGetServiceAmountDataService
    {
        private readonly DataServiceSettings _serviceSettings;
        private readonly IServiceRequestConfiguration _requestConfiguration;
        private readonly HttpClient _client;

        public GetServiceAmountDataService(
            IOptions<DataServices> serviceSettings,
            IServiceRequestConfiguration requestConfiguration,
            IHttpClientFactory httpClientFactory)
        {
            _serviceSettings = serviceSettings.Value.GetDataServiceSettings(GetType().Name);
            _requestConfiguration = requestConfiguration;
            _client = httpClientFactory.CreateClient(GetType().Name);
        }

        /// <inheritdoc/>
        public async Task<int> GetServiceAmount(string serviceId, CancellationToken cancellationToken)
        {
            string[] urlParameter = { serviceId };
            var (requestMethod, requestUri) = _serviceSettings.GetEndPoint("GetServiceAmount", urlParameter);
            using var request = new HttpRequestMessage(requestMethod, requestUri);

            using var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("service call failed");
            }

            if (response.Content == null)
            {
                throw new Exception("Invalid service response");
            }

            return Int32.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }
    }
}
