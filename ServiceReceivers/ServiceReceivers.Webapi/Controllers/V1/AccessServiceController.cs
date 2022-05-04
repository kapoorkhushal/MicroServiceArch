using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceReceivers.Application.Features;
using ServiceReceivers.Domain.DTOs;
using System.Threading.Tasks;

namespace ServiceReceivers.Webapi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class AccessServiceController : BaseApiController
    {
        private readonly ILogger<AccessServiceController> _logger;

        public AccessServiceController(ILogger<AccessServiceController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Access services
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AccessServices(AccessServicesInputDTO input)
        {
            var result = await Mediator.Send(new UserAccessServices { 
                UserId = input.UserId,
                ServiceIds = input.ServiceIds
            });

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
