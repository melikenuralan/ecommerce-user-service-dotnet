using MediatR;

namespace UserService.Application.Features.Commands.UserAuth.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
