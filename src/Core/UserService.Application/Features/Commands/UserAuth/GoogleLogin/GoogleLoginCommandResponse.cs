using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTOs;

namespace UserService.Application.Features.Commands.UserAuth.GoogleLogin
{
    public class GoogleLoginCommandResponse
    {
        public TokenDto Token { get; set; }
        public GoogleUserDto User { get; set; }
    }
}
