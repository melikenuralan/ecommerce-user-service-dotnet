using UserService.Application.DTOs.Auth;
using UserService.Domain.Enums;

namespace UserService.Application.Features.Commands.UserAuth.LoginUser
{
    public class LoginUserCommandResponse
    {
        public bool Succeess { get; set; }
        public string Message { get; set; }
        public TokenDto? Token { get; set; }


        public bool RequiresTwoFactor { get; set; }
        public TwoFactorType? TwoFactorType { get; set; }

        public static LoginUserCommandResponse SuccessResult(TokenDto token) =>
            new() { Succeess = true, Message = "Başarılı giriş", Token = token };

        public static LoginUserCommandResponse Fail(string message) =>
            new() { Succeess = false, Message = message };
    }

}
