using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceProviders.Application.Features;
using ServiceProviders.Domain.DTOs;
using System;
using System.Threading.Tasks;

namespace ServiceProviders.Webapi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class ServiceProvidersController : BaseApiController
    {
        private readonly ILogger<ServiceProvidersController> _logger;

        public ServiceProvidersController(ILogger<ServiceProvidersController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// engage the status of service provider
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EngageServiceProvider(EngageServiceProviderInputDTO input)
        {
            var result = await Mediator.Send(
                new EngageServiceProviderStatus
                {
                    UserId = input.UserId,
                    ServiceProviderId = input.ServiceProviderId
                });

            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        /// <summary>
        /// release the status of service provider
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ReleaseServiceProvider(Guid id)
        {
            var result = await Mediator.Send(new ReleaseServiceProviderStatus { ServiceProviderId = id });

            if (result == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
