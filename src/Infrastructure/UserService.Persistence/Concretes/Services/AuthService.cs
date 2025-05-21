using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;
using UserService.Domain;
using UserService.Domain.Interfaces;
using UserService.Domain.ValueObjects;
using UserService.Persistence.Identity;

namespace UserService.Persistence.Concretes.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenService;

        public AuthService(UserManager<AppUser> userManager, IUserRepository userRepository, ITokenProvider tokenService)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }


        public async Task<AuthResultDto> LoginAsync(LoginRequestDto request)
        {
            AppUser? appUser = await _userManager.FindByNameAsync(request.Username);
            if (appUser == null)
                return AuthResultDto.Failure("Kullanıcı adı veya şifre hatalı.");
            bool passwordValid = await _userManager.CheckPasswordAsync(appUser, request.Password);
            if (!passwordValid)
                return AuthResultDto.Failure("Kullanıcı adı veya şifre hatalı.");

            IList<string> roles = await _userManager.GetRolesAsync(appUser);

            TokenDto token = _tokenService.GenerateToken(
                60,
                appUser.Id.ToString(),
                appUser.UserName,
                roles
            );

            return AuthResultDto.Success(token);
        }
        private string GenerateReferralCode()
        {
            return Guid.NewGuid().ToString("N")[..8].ToUpper(); // 8 karakterlik benzersiz bir kod
        }


        public async Task<AuthResultDto> RegisterAsync(RegisterRequestDto request)
        {
            try
            {
                if (request.Password != request.PasswordConfirm)
                    return AuthResultDto.Failure("Şifreler uyuşmuyor.");

                var appUser = new AppUser
                {
                    Id = Guid.NewGuid(),
                    Email = request.Email,
                    UserName = request.Username,
                    IsEmailVerified = false,
                    IsPhoneVerified = false,
                    ReferralCode = GenerateReferralCode()
                };

                IdentityResult identityResult = await _userManager.CreateAsync(appUser, request.Password);
                if (!identityResult.Succeeded)
                {
                    List<string> errors = identityResult.Errors
                        .Select(e => e.Description)
                        .ToList();

                    return AuthResultDto.Failure(errors.ToArray());
                }

                var roles = await _userManager.GetRolesAsync(appUser);

                TokenDto token = _tokenService.GenerateToken(
                    60,
                    appUser.Id.ToString(),
                    appUser.UserName,
                    roles
                );

                return AuthResultDto.Success(token);
            }
            catch (Exception ex)
            {
                throw new Exception("Bir hata oluştu.", ex);
            }
        }
    }
}
