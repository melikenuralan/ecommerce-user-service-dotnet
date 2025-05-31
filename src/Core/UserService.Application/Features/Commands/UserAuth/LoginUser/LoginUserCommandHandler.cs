using MediatR;
using UserService.Application.Abstractions;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;

namespace UserService.Application.Features.Commands.UserAuth.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILogService _logger;
        private readonly ICaptchaService _captchaService;

        public LoginUserCommandHandler(IAuthService authService, ILogService logger, ICaptchaService captchaService)
        {
            _authService = authService;
            _logger = logger;
            _captchaService = captchaService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            // reCAPTCHA doğrulama
            var isHuman = await _captchaService.VerifyTokenAsync(request.RecaptchaToken);
            if (!isHuman)
            {
                _logger.Warning($"[LOGIN] reCAPTCHA doğrulaması başarısız: {request.UsernameOrEmail}");
                return LoginUserCommandResponse.Fail("reCAPTCHA doğrulaması geçersiz.");
            }          
            _logger.Info($"[LOGIN] Giriş denemesi: {request.UsernameOrEmail}");

            AuthResultDto result = await _authService.LoginAsync(new LoginRequestDto
            {
                UsernameOrEmail = request.UsernameOrEmail,
                Password = request.Password
            });

            if (result.RequiresTwoFactor)
            {
                _logger.Info($"[LOGIN] 2FA gerekli: {request.UsernameOrEmail}");
                return new LoginUserCommandResponse
                {
                    Succeess = false,
                    Message = "İki adımlı doğrulama gerekli.",
                    RequiresTwoFactor = true,
                    TwoFactorType = result.TwoFactorType
                };
            }

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
