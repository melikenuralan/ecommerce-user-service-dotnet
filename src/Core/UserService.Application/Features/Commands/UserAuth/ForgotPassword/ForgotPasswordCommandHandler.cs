using MediatR;
using Shared.Contracts.Events.Users;
using UserService.Application.Abstractions.IServices;
using UserService.Application.Abstractions.Messaging;

namespace UserService.Application.Features.Commands.UserAuth.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommandRequest, ForgotPasswordCommandResponse>
    {
        private readonly IAuthService _authService;
        private readonly IEventPublisher _eventPublisher;

        public ForgotPasswordCommandHandler(IAuthService authService, IEventPublisher eventPublisher)
        {
            _authService = authService;
            _eventPublisher = eventPublisher;
        }

        public async Task<ForgotPasswordCommandResponse> Handle(ForgotPasswordCommandRequest request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.ResetLink))
                throw new ArgumentException("Email veya reset link boş olamaz.");

            await _authService.ProcessForgotPasswordAsync(request.Email, request.ResetLink);

          

            await _eventPublisher.PublishAsync(new PasswordResetEvent
            {
                Email = request.Email,
                ResetLink = request.ResetLink
            });

            return new ForgotPasswordCommandResponse
            {
                Success = true,
                Message = "Şifre sıfırlama bağlantısı e-posta adresinize gönderildi."
            };
        }
    }
}
