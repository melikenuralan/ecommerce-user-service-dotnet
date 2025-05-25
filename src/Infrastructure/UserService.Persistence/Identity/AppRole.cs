using Microsoft.AspNetCore.Identity;

namespace UserService.Persistence.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole() : base() { }
        public AppRole(string roleName) : base(roleName)
        {
        }
    }
}
