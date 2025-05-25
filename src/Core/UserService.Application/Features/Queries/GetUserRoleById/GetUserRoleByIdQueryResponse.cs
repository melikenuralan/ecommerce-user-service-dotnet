using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Features.Queries.GetUserRoleById
{
    public class GetUserRoleByIdQueryResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public IList<string> Roles { get; set; }
        public IList<Claim> Claims { get; set; }
    }
}
