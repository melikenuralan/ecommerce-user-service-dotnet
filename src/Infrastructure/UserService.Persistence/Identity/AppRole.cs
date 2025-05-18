using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UserService.Persistence.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        private AppRole() { }

        public AppRole(string roleName) : base(roleName)
        {
        }
    }
}
