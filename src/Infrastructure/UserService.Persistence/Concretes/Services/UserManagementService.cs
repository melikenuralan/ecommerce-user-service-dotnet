using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<AppUserDto> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null ? null : MapToDto(user);
        }

        public async Task<AppUserDto> FindByExternalLoginAsync(string loginProvider, string providerKey)
        {
            var user = await _userManager.FindByLoginAsync(loginProvider, providerKey);
            return user == null ? null : MapToDto(user);
        }

        public async Task<(bool Succeeded, string[] Errors)> CreateAsync(AppUserDto userDto)
        {
            var user = new AppUser
            {
                Email = userDto.Email,
                UserName = userDto.UserName
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description).ToArray());
            }

            userDto.Id = user.Id; // ID'yi geriye dönmek için ayarla
            return (true, Array.Empty<string>());
        }

        public async Task AddLoginAsync(Guid userId, string loginProvider, string providerKey)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            var loginInfo = new UserLoginInfo(loginProvider, providerKey, loginProvider);
            var result = await _userManager.AddLoginAsync(user, loginInfo);

            if (!result.Succeeded)
                throw new Exception($"Login eklenemedi: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        public async Task<IList<string>> GetRolesAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return await _userManager.GetRolesAsync(user);
        }

        private AppUserDto MapToDto(AppUser user)
        {
            return new AppUserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };
        }
    }
}
