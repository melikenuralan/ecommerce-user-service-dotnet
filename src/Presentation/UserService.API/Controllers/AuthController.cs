using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Features.Commands.UserAuth.GoogleLogin;
using UserService.Application.Features.Commands.UserAuth.LoginUser;
using UserService.Application.Features.Commands.UserAuth.RegisterUser;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = "Admin")]

    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;


        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            LoginUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            RegisterUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
