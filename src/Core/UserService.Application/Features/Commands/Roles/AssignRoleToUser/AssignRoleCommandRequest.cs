using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace UserService.Application.Features.Commands.Roles.AssignRoleToUser
{
    public class AssignRoleCommandRequest: IRequest<AssignRoleCommandResponse>
    {
        public Guid UserId { get; set; }
        public string[] Roles { get; set; }
    }
}
