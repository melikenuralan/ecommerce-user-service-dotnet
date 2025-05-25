using System.Data;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;
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

        public async Task<RoleDto> CreateRoleAsync(string name)
        {
            AppRole role = new AppRole(name);
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                string errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Role creation failed: {errors}");
            }
            RoleDto dto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = new List<string>()
            };
            return dto;
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
        public async Task<RoleDto> UpdateRoleAsync(Guid id, string name)
        {
            AppRole? role = await _roleManager.FindByIdAsync(id.ToString());
            if (role is null)
                throw new KeyNotFoundException($"Role with Id {id} not found.");

            role.Name = name;

            IdentityResult result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                string errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Role update failed: {errors}");
            }

            RoleDto dto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Permissions =
                    new List<string>()
            };
            return dto;
        }
    }
}
