using Microsoft.AspNetCore.Identity;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;
using UserService.Persistence.Identity;

namespace UserService.Persistence.Concretes.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserManagementService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AppUserDto> FindByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            return new AppUserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
              //  ReferralCode = user.ReferralCode,
                TwoFactorType = user.TwoFactorType,
                TwoFactorEnabled = user.TwoFactorEnabled,

            };
        }

        public async Task<bool> VerifyAuthenticatorCodeAsync(AppUserDto userDto, string verificationCode)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString())
                       ?? throw new Exception("Kullanıcı bulunamadı.");

            return await _userManager.VerifyTwoFactorTokenAsync(
                user,
                _userManager.Options.Tokens.AuthenticatorTokenProvider,
                verificationCode);
        }

        public async Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(AppUserDto userDto, int number)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString())
                       ?? throw new Exception("Kullanıcı bulunamadı.");

            return await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, number);
        }


        public async Task UpdateAsync(AppUserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString())
                       ?? throw new Exception("Kullanıcı bulunamadı.");

            user.Email = userDto.Email;
            user.UserName = userDto.UserName;
          //  user.ReferralCode = userDto.ReferralCode;
            user.TwoFactorType = userDto.TwoFactorType;
            user.TwoFactorEnabled=userDto.TwoFactorEnabled;

            await _userManager.UpdateAsync(user);
        }

        public async Task ResetAuthenticatorKeyAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                       ?? throw new Exception("Kullanıcı bulunamadı.");
            await _userManager.ResetAuthenticatorKeyAsync(user);
        }

        public async Task<string?> GetAuthenticatorKeyAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                       ?? throw new Exception("Kullanıcı bulunamadı.");
            return await _userManager.GetAuthenticatorKeyAsync(user);
        }


        public async Task<AppUserDto> FindByEmailAsync(string email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            return user == null ? null : MapToDto(user);
        }

        public async Task<AppUserDto> FindByExternalLoginAsync(string loginProvider, string providerKey)
        {
            AppUser? user = await _userManager.FindByLoginAsync(loginProvider, providerKey);
            return user == null ? null : MapToDto(user);
        }

        public async Task<(bool Succeeded, string[] Errors)> CreateAsync(AppUserDto userDto)
        {
            AppUser? user = new AppUser
            {
                Email = userDto.Email,
                UserName = userDto.UserName
            };

            IdentityResult? result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description).ToArray());
            }

            userDto.Id = user.Id; // ID'yi geriye dönmek için ayarla
            return (true, Array.Empty<string>());
        }

        public async Task AddLoginAsync(Guid userId, string loginProvider, string providerKey)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            UserLoginInfo loginInfo = new UserLoginInfo(loginProvider, providerKey, loginProvider);
            IdentityResult? result = await _userManager.AddLoginAsync(user, loginInfo);

            if (!result.Succeeded)
                throw new Exception($"Login eklenemedi: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        public async Task<IList<string>> GetRolesAsync(Guid userId)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId.ToString());
            return await _userManager.GetRolesAsync(user);
        }

        private AppUserDto MapToDto(AppUser user)
        {
            return new AppUserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                TwoFactorType = user.TwoFactorType,
                TwoFactorEnabled=user.TwoFactorEnabled,
            };
        }
    }
}
