using System.Data;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using UserService.Application.Abstractions.IExternalServices;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;
namespace UserService.Infrastructure.ExternalServices
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly string _clientId;
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserManagementService _userManagementService;

        public GoogleAuthService(
            IConfiguration configuration,
            ITokenProvider tokenProvider,
            IUserManagementService userManagementService)
        {
            _clientId = configuration["Authentication:Google:ClientId"];
            _tokenProvider = tokenProvider;
            _userManagementService = userManagementService;
        }

        public async Task<GoogleLoginResultDto> LoginAsync(string idToken)
        {
            var payload = await ValidateAsync(idToken);

            var userDto = await _userManagementService.FindByExternalLoginAsync(payload.Provider, payload.Subject)
                         ?? await _userManagementService.FindByEmailAsync(payload.Email);

            if (userDto == null)
            {
                userDto = new AppUserDto
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    ReferralCode = Guid.NewGuid().ToString("N")
                };

                var (succeeded, errors) = await _userManagementService.CreateAsync(userDto);
                if (!succeeded)
                    throw new ApplicationException(string.Join(", ", errors));

                await _userManagementService.AddLoginAsync(userDto.Id, payload.Provider, payload.Subject);
            }

            var roles = await _userManagementService.GetRolesAsync(userDto.Id);
        //    userDto.Roles = roles;

            var token = _tokenProvider.GenerateToken(
                minute: 60,
                userId: userDto.Id.ToString(),
                userName: userDto.UserName,
                roles: roles
            );

            return new GoogleLoginResultDto
            {
                Token = token,
                Payload = payload
            };
        }

        private async Task<GooglePayload> ValidateAsync(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { _clientId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            return new GooglePayload
            {
                Provider = "Google",
                Subject = payload.Subject,
                Email = payload.Email,
                Name = payload.Name,
                FirstName = payload.GivenName,
                LastName = payload.FamilyName,
                PhotoUrl = payload.Picture
            };
        }
    }
}