using UserService.Application.DTOs;

namespace UserService.Application.Features.Commands.UserAuth.GoogleLogin
{
    public class GoogleLoginCommandResponse
    {
        public TokenDto Token { get; set; }
        public GoogleUserDto User { get; set; }
    }
}
