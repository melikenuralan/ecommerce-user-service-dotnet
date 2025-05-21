using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using UserService.Application.Features.Commands.UserAuth.LoginUser;
using UserService.Application.Features.Commands.UserAuth.RegisterUser;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class AuthController : ControllerBase
    {
        private readonly LoginUserCommandHandler _loginHandler;
        private readonly RegisterUserCommandHandler _registerHandler;

        public AuthController(LoginUserCommandHandler loginHandler, RegisterUserCommandHandler registerHandler = null)
        {
            _loginHandler = loginHandler;
            _registerHandler = registerHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _loginHandler.HandleAsync(request, cancellationToken);
            if (!result.Success)
                return BadRequest(new { error = result.Message });

            return Ok(result.Token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _registerHandler.HandleAsync(request, cancellationToken);
            if (!result.Succeess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
