using MediatR;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;

namespace UserService.Application.Features.Commands.UserAuth.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILogService _logger;

        public LoginUserCommandHandler(IAuthService authService, ILogService logger = null)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.Info($"[LOGIN] Giriş denemesi: {request.UsernameOrEmail}");

            AuthResultDto result = await _authService.LoginAsync(new LoginRequestDto
            {
                UsernameOrEmail = request.UsernameOrEmail,
                Password = request.Password
            });

            if (!result.Succeeded)
            {
                string message = result.Errors?.FirstOrDefault() ?? "Bilinmeyen bir hata oluştu.";
                _logger.Warning($"[LOGIN] Başarısız giriş: {message}");
                return LoginUserCommandResponse.Fail(message);
            }

            _logger.Info($"[LOGIN] Başarılı giriş: {request.UsernameOrEmail}");
            return LoginUserCommandResponse.SuccessResult(result.Token!);
        }
    }
}
