using Microsoft.Extensions.Options;
using ServiceProviders.Application.Interfaces;
using ServiceProviders.Domain.Configuration;
using ServiceProviders.Domain.DataServiceConfiguration;
using ServiceProviders.Domain.DTOs;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceProviders.Infrastructure.DataService
{
    public class GetUserDetailsDataService : IGetUserDetailsDataService
    {
        private readonly DataServiceSettings _serviceSettings;
        private readonly IServiceRequestConfiguration _requestConfiguration;
        private readonly HttpClient _client;

        public GetUserDetailsDataService(
            IOptions<DataServices> serviceSettings,
            IServiceRequestConfiguration requestConfiguration,
            IHttpClientFactory httpClientFactory)
        {
            _serviceSettings = serviceSettings.Value.GetDataServiceSettings(GetType().Name);
            _requestConfiguration = requestConfiguration;
            _client = httpClientFactory.CreateClient(GetType().Name);
        }

        /// <inheritdoc/>
        public async Task<GetUserByIdOutputDTO> GetUserDetails(string userId, CancellationToken cancellationToken)
        {
            string[] urlParameter = { userId };
            var (requestMethod, requestUri) = _serviceSettings.GetEndPoint("GetUser", urlParameter);
            using var request = new HttpRequestMessage(requestMethod, requestUri);

            using var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("service call failed");
            }

            if (response.Content == null)
            {
                throw new Exception("Invalid service response");
            }

            var result =  await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<GetUserByIdOutputDTO>(result);
        }
    }
}
