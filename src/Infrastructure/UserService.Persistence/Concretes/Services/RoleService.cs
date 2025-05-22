using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Abstractions.IServices;
using UserService.Persistence.Identity;

namespace UserService.Persistence.Concretes.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> CreateRoleAsync(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new AppRole(name));
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(Guid id)
        {
            AppRole? appRole = await _roleManager.FindByIdAsync(id.ToString());
            IdentityResult result = await _roleManager.DeleteAsync(appRole);
            return result.Succeeded;
        }

        public IDictionary<Guid, string> GetAllRoles()
        {
            return _roleManager.Roles.ToDictionary(role => role.Id, role => role.Name);

        }
        public async Task<(Guid id, string name)> GetRoleByIdAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return (id, role.Name);
        }
        public async Task<bool> UpdateRoleAsync(Guid id, string name)
        {
            AppRole? role = await _roleManager.FindByIdAsync(id.ToString());
            role.Name = name;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}
