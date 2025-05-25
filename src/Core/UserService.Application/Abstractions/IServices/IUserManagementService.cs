using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTOs;

namespace UserService.Application.Abstractions.IServices
{
    public interface IUserManagementService
    {
        Task<AppUserDto> FindByEmailAsync(string email);
        Task<AppUserDto> FindByExternalLoginAsync(string loginProvider, string providerKey);
        Task<(bool Succeeded, string[] Errors)> CreateAsync(AppUserDto user);
        Task AddLoginAsync(Guid userId, string loginProvider, string providerKey);
        Task<IList<string>> GetRolesAsync(Guid userId);
    }
}
