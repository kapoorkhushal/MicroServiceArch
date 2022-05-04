using ServiceProviders.Domain.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceProviders.Application.Interfaces
{
    public interface IGetUserDetailsDataService
    {
        /// <summary>
        /// Get user details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<GetUserByIdOutputDTO> GetUserDetails(string userId, CancellationToken cancellationToken);
    }
}
