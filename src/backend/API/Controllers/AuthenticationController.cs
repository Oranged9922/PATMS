using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using Contracts.Authentication;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller dedicated to handling Register and Login requests.
    /// </summary>
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        private readonly ISender mediator;
        private readonly IMapper mapper;

        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Handles register request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            RegisterCommand command = mapper.Map<RegisterCommand>(request);
            ErrorOr<AuthenticationResult> authResult = await mediator.Send(command);
            return authResult.Match(
                authResult => Ok(mapper.Map<AuthenticationResponse>(authResult)),
                Problem);
        }

        /// <summary>
        /// Handles login request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            LoginQuery query = mapper.Map<LoginQuery>(request);
            ErrorOr<AuthenticationResult> authResult = await mediator.Send(query);

            return authResult.Match(
                authResult => Ok(mapper.Map<AuthenticationResponse>(authResult)),
                Problem);
        }
    }
}
