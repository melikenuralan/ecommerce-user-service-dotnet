using MediatR;
using UserService.Application.Abstractions.IServices;
using UserService.Application.Abstractions.Messaging;
using UserService.Application.DTOs;
using UserService.Application.Events;

namespace UserService.Application.Features.Commands.UserAuth.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILogService _logger;
        private readonly IEventPublisher _eventPublisher;
        public RegisterUserCommandHandler(IAuthService authService, ILogService logger, IEventPublisher eventPublisher)
        {
            _authService = authService;
            _logger = logger;
            _eventPublisher = eventPublisher;
        }

        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            _logger.Info($"[REGISTER] Yeni kullanıcı kayıt denemesi: {request.Username}");

          

            AuthResultDto response = await _authService.RegisterAsync(new()
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                Username = request.Username,
                TwoFactorType = request.TwoFactorType
            });

            if (!response.Succeeded)
            {
                string joinedErrors = string.Join(" | ", response.Errors!);
                _logger.Warning($"[REGISTER] Kayıt başarısız: {joinedErrors}");
            }
            else
            {
                _logger.Info($"[REGISTER] Kayıt başarılı: {request.Username}");

                var userCreatedEvent = new UserCreatedEvent
                {
                    Email = request.Email,
                    UserName = request.Username
                };
                _logger.Info($"[EVENT] UserCreatedEvent fırlatılıyor: {request.Email}");
          
                await _eventPublisher.PublishAsync(userCreatedEvent);
            }

            return new RegisterUserCommandResponse
            {
                Succeess = response.Succeeded,
                Message = response.Succeeded ? "Kayıt başarılı" : string.Join(" | ", response.Errors!)
            };
        }
    }
}