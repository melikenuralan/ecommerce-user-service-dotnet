using UserService.Application.DTOs.Identity;

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

        Task<AppUserDto> FindByIdAsync(string userId);
        Task<bool> VerifyAuthenticatorCodeAsync(AppUserDto userDto, string verificationCode);
        Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(AppUserDto user, int number);
        Task UpdateAsync(AppUserDto user);


    }
}
