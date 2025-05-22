using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Features.Commands.Roles.CreateRole
{
    public class CreateRoleCommandResponse
    {
        public Guid Id { get; set; }
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}
