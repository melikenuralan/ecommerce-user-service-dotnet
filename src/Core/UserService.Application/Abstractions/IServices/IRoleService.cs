using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTOs;

namespace UserService.Application.Abstractions.IServices
{
    public interface IRoleService
    {
        IDictionary<Guid, string> GetAllRoles();
        Task<(Guid id, string name)> GetRoleByIdAsync(Guid id);
        Task<RoleDto> CreateRoleAsync(string name);
        Task<RoleDto> UpdateRoleAsync(Guid id, string name);
        Task<bool> DeleteRoleAsync(Guid id);
    }
}