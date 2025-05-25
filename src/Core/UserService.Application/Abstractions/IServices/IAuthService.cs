using UserService.Application.DTOs;

namespace UserService.Application.Abstractions.IServices
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResultDto> LoginAsync(LoginRequestDto request);
    }
}
