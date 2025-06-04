using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;
using UserService.Application.DTOs.Auth;
using UserService.Application.DTOs.Identity;
using UserService.Domain.Entities;
using UserService.Domain.Enums;
using UserService.Persistence.Identity;

namespace UserService.Persistence.Concretes.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenProvider _tokenService;

        public AuthService(UserManager<AppUser> userManager, ITokenProvider tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public async Task AssignRoleToUserAsync(Guid userId, string[] roles)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(userId.ToString());
            if (appUser != null)
            {
                var userRoles = await _userManager.GetRolesAsync(appUser);
                await _userManager.RemoveFromRolesAsync(appUser, userRoles);
                await _userManager.AddToRolesAsync(appUser, roles);
            }
        }
        public async Task<UserRoleDto> GetUserRoleByIdsAsync(Guid id)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(id.ToString());

            if (appUser is null)
                throw new Exception("Kullanıcı bulunamadı.");

            var roles = await _userManager.GetRolesAsync(appUser);
            var claims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            return new UserRoleDto
            {
                UserId = id,
                UserName = appUser.UserName,
                Roles = roles,
                Claims = claims
            };
        }

        public async Task<AuthResultDto> LoginAsync(LoginRequestDto request)
        {
            AppUser? appUser = await _userManager.FindByNameAsync(request.UsernameOrEmail)
                             ?? await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (appUser is null)
                return AuthResultDto.Failure("Kullanıcı adı veya şifre hatalı.");

            SignInResult signInResult = await _signInManager
                .CheckPasswordSignInAsync(appUser, request.Password, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
                return AuthResultDto.Failure("Kullanıcı adı veya şifre hatalı.");

            if (await _userManager.GetTwoFactorEnabledAsync(appUser))
            {
                return AuthResultDto.Require2FA(appUser.TwoFactorType);
            }


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
            return Guid.NewGuid().ToString("N")[..8].ToUpper();
        }


        public async Task<AuthResultDto> RegisterAsync(RegisterRequestDto request)
        {
            if (request.Password != request.PasswordConfirm)
                return AuthResultDto.Failure("Şifreler uyuşmuyor.");

            AppUser appUser = new AppUser
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

            IList<string> roles = await _userManager.GetRolesAsync(appUser);

            TokenDto token = _tokenService.GenerateToken(
                60,
                appUser.Id.ToString(),
                appUser.UserName,
                roles
            );

            return AuthResultDto.Success(token);
        }

        public async Task ProcessForgotPasswordAsync(string email,string resetLink)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            // Token üret
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }
    }
}
