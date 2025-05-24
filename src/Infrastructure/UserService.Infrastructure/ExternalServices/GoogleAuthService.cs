using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using UserService.Application.Abstractions.IExternalServices;
using UserService.Application.DTOs;

namespace UserService.Infrastructure.ExternalServices
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly string _clientId;

        public GoogleAuthService(IConfiguration configuration)
        {
            _clientId = configuration["Authentication:Google:ClientId"];
        }

        public async Task<GooglePayload> ValidateAsync(string idToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _clientId }
            });

            return new GooglePayload
            {
                Subject = payload.Subject,
                Email = payload.Email,
                Name = payload.Name,
           
            };
        }
    }
}
