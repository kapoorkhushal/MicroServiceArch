using ServiceProviders.Domain.Models;
using System.Collections.Generic;

namespace ServiceProviders.Application.Interfaces
{
    public interface IProvidersDataService
    {
        /// <summary>
        /// Get the list of service providers
        /// </summary>
        /// <returns></returns>
        List<Provider> GetAll();
    }
}
