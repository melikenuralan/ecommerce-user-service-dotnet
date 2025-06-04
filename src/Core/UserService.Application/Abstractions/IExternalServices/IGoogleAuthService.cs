using UserService.Application.DTOs.Google;

namespace UserService.Application.Abstractions.IExternalServices
{
    public interface IGoogleAuthService
    {
        Task<GoogleLoginResultDto> LoginAsync(string idToken);
    }

}
