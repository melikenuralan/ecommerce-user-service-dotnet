using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Abstractions.IServices
{
    public interface IRoleService
    {
        IDictionary<Guid, string> GetAllRoles();
        Task<(Guid id, string name)> GetRoleById(Guid id);
        Task<bool> CreateRole(string name);
        Task<bool> UpdateRole(Guid id, string name);
        Task<bool> DeleteRole(Guid id);
    }
}
