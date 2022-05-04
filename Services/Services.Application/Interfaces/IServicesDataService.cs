using Services.Domain;
using System.Collections.Generic;

namespace Services.Application.Interfaces
{
    public interface IServicesDataService
    {
        /// <summary>
        /// Get all the list of services
        /// </summary>
        /// <returns></returns>
        List<Service> GetAll();
    }
}
