using UserService.Application.DTOs.Auth;
using UserService.Application.DTOs.Google;

namespace UserService.Application.Features.Commands.UserAuth.GoogleLogin
{
    public class GoogleLoginCommandResponse
    {
        public TokenDto Token { get; set; }
        public GoogleUserDto User { get; set; }
    }
}
