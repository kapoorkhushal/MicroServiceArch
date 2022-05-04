using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceReceivers.Application.Interfaces
{
    public interface IGetServiceAmountDataService
    {
        /// <summary>
        /// Get the amount of the service from Services microservice
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        Task<int> GetServiceAmount(string serviceId, CancellationToken cancellationToken);
    }
}
