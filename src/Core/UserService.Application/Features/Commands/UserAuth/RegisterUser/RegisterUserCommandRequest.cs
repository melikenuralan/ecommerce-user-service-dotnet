using MediatR;
using UserService.Domain.Enums;

namespace UserService.Application.Features.Commands.UserAuth.RegisterUser
{
    public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResponse>
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public TwoFactorType TwoFactorType { get; set; }
    }
}
