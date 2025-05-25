using MediatR;

namespace UserService.Application.Features.Commands.Roles.CreateRole
{
    public class CreateRoleCommandRequest : IRequest<CreateRoleCommandResponse>
    {
        public string Name { get; set; } = string.Empty;
    }
}
