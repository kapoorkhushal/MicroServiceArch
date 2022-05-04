using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Application.Features;
using System;
using System.Threading.Tasks;

namespace Services.Webapi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class GetAllServicesController : BaseApiController
    {
        private readonly ILogger<GetAllServicesController> _logger;

        public GetAllServicesController(ILogger<GetAllServicesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets all Services.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllServices()));
        }

        /// <summary>
        /// Gets Service amount
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAmount(Guid id)
        {
            return Ok(await Mediator.Send(new GetServiceAmount { ServiceId = id }));
        }

    }
}
