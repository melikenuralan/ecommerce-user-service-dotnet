using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserService.Domain.ValueObjects;

namespace UserService.Application.Features.Commands.UserAuth.RegisterUser
{
    public class RegisterUserCommandRequest
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
