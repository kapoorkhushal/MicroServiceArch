using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceProviders.Application.Features;
using ServiceProviders.Domain.DTOs;
using System.Threading.Tasks;

namespace ServiceProviders.Webapi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class AccessServiceProviderController : BaseApiController
    {
        private readonly ILogger<AccessServiceProviderController> _logger;

        public AccessServiceProviderController(ILogger<AccessServiceProviderController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get the list of available service providers
        /// </summary>
        /// <param name="pincode"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetAvailableServiceProviders(AccessServiceProvidersInputDTO input)
        {
            return base.Ok(await Mediator.Send(new AccessServiceProvider { 
                Pincode = input.Pincode, 
                ServiceId = input.ServiceId, 
                UserId = input.UserId 
            }));
        }
    }
}
