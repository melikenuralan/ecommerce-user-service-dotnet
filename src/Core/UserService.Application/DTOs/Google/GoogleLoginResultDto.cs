using UserService.Application.DTOs.Auth;

namespace UserService.Application.DTOs.Google
{
    public class GoogleLoginResultDto
    {
        public TokenDto Token { get; set; }
        public GooglePayload Payload { get; set; }
    }
}
