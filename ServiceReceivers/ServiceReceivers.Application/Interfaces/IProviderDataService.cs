using ServiceReceivers.Domain.DTOs;
using System.Threading.Tasks;

namespace ServiceReceivers.Application.Interfaces
{
    public interface IProviderDataService
    {
        /// <summary>
        /// request the service
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> AccessService(AccessServiceProvidersDTO input);
    }
}
