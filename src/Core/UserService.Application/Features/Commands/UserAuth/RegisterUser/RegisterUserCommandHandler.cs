using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;
using UserService.Domain.ValueObjects;

namespace UserService.Application.Features.Commands.UserAuth.RegisterUser
{
    public class RegisterUserCommandHandler
    {
        private readonly IAuthService _authService;
        public RegisterUserCommandHandler(IAuthService authService) => _authService = authService;

        public async Task<RegisterUserCommandResponse> HandleAsync(RegisterUserCommandRequest request,CancellationToken cancellationToken)
        {
            AuthResultDto response = await _authService.RegisterAsync(new()
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                Username = request.Username
            });

            return new RegisterUserCommandResponse
            {
                Succeeded = response.Succeeded,
                Message = response.Succeeded ? "Kayıt başarılı" : string.Join(" | ", response.Errors!)
            };
        }

    }
}
