using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Features.Commands.UserAuth.LoginUser
{
    public class LoginUserCommandRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
