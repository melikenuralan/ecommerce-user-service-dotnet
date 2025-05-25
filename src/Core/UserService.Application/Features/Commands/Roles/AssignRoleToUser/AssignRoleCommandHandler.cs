using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UserService.Application.Abstractions.IServices;

namespace UserService.Application.Features.Commands.Roles.AssignRoleToUser
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommandRequest, AssignRoleCommandResponse>
    {
        readonly IAuthService _authService;

        public AssignRoleCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AssignRoleCommandResponse> Handle(AssignRoleCommandRequest request, CancellationToken cancellationToken)
        {
            await _authService.AssignRoleToUserAsync(request.UserId, request.Roles);

            return new();
        }
    }
}
