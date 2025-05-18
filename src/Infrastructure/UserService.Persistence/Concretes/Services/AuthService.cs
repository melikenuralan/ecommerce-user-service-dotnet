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


        public Task<AuthResultDto> LoginAsync(LoginRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResultDto> RegisterAsync(RegisterRequestDto request)
        {
            if (request.Password != request.PasswordConfirm)
                return AuthResultDto.Failure("Şifreler uyuşmuyor.");

            var appUser = new AppUser
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                UserName = request.Username,
                IsEmailVerified = false,
                IsPhoneVerified = false
            };

            IdentityResult identityResult = await _userManager.CreateAsync(appUser, request.Password);
            if (!identityResult.Succeeded)
            {
                List<string> errors = identityResult.Errors
                    .Select(e => e.Description)
                    .ToList();

                return AuthResultDto.Failure(errors.ToArray());
            }

            var domainUser = new Domain.Entities.User(
                appUser.Id,
                new Email(request.Email),
                new FullName(request.FullName)
            );

            await _userRepository.AddAsync(domainUser);

            var roles = await _userManager.GetRolesAsync(appUser);

            TokenDto token = _tokenService.GenerateToken(
                60,
                appUser.Id.ToString(),
                appUser.UserName,
                roles
            );

            return AuthResultDto.Success(token);
        }
    }
}
