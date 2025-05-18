using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;

namespace UserService.Application.Features.Commands.UserAuth.LoginUser
{
    public class LoginUserCommandHandler
    {
        private readonly IAuthService _authService;
        private readonly ILogService _logger;

        public LoginUserCommandHandler(IAuthService authService, ILogService logger = null)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<LoginUserCommandResponse> HandleAsync(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.Info($"[LOGIN] Giriş denemesi: {request.Username}");

            var result = await _authService.LoginAsync(new LoginRequestDto
            {
                Username = request.Username,
                Password = request.Password
            });

            if (!result.Succeeded)
            {
                string message = result.Errors?.FirstOrDefault() ?? "Bilinmeyen bir hata oluştu.";
                _logger.Warning($"[LOGIN] Başarısız giriş: {message}");
                return LoginUserCommandResponse.Fail(message);
            }

            _logger.Info($"[LOGIN] Başarılı giriş: {request.Username}");
            return LoginUserCommandResponse.SuccessResult(result.Token!);
        }

    }
}
