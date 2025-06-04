using UserService.Application.DTOs;
using UserService.Application.DTOs.Auth;
using UserService.Application.DTOs.Identity;
using UserService.Application.Features.Queries.GetUserRoleById;

namespace UserService.Application.Abstractions.IServices
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResultDto> LoginAsync(LoginRequestDto request);
        Task AssignRoleToUserAsync(Guid userId, string[] roles);
        Task<UserRoleDto> GetUserRoleByIdsAsync(Guid id);
    }
}
