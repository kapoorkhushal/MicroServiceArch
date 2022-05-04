using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceReceivers.Application.Features;
using System;
using System.Threading.Tasks;

namespace ServiceReceivers.Webapi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class UserController : BaseApiController
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserService user)
        {
            return Ok(await Mediator.Send(user));
        }

        /// <summary>
        /// Get user from Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetUserByIdService { Id = id }));
        }

    }
}
