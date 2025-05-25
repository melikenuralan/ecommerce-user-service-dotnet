using UserService.Application.DTOs;

namespace UserService.Application.Abstractions.IExternalServices
{
    public interface IGoogleAuthService
    {
        Task<GoogleLoginResultDto> LoginAsync(string idToken);
    }

}
