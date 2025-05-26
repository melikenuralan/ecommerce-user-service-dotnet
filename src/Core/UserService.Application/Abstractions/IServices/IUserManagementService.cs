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
        Task<string> GetAuthenticatorKeyAsync(Guid userId);
        Task ResetAuthenticatorKeyAsync(Guid userId);
    }
}
