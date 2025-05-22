using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Features.Commands.Roles.CreateRole
{
    public class CreateRoleCommandRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
