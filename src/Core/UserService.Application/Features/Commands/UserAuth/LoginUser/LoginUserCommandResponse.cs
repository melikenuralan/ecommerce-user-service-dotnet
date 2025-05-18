using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTOs;

namespace UserService.Application.Features.Commands.UserAuth.LoginUser
{
    public class LoginUserCommandResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public TokenDto? Token { get; set; }

        public static LoginUserCommandResponse Fail(string message)
        {
            return new LoginUserCommandResponse
            {
                Success = false,
                Message = message
            };
        }

        public static LoginUserCommandResponse SuccessResult(TokenDto token)
        {
            return new LoginUserCommandResponse
            {
                Success = true,
                Token = token
            };
        }
    }
}
