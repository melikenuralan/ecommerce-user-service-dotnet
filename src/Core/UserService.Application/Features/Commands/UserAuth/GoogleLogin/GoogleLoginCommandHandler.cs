using MediatR;
using UserService.Application.Abstractions.IExternalServices;
using UserService.Application.DTOs;

namespace UserService.Application.Features.Commands.UserAuth.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly IGoogleAuthService _googleAuth;

        public GoogleLoginCommandHandler(IGoogleAuthService googleAuth)
            => _googleAuth = googleAuth;

        public async Task<GoogleLoginCommandResponse> Handle(
            GoogleLoginCommandRequest request,
            CancellationToken cancellationToken)
        {
            GoogleLoginResultDto result = await _googleAuth.LoginAsync(request.IdToken);

            GoogleUserDto userDto = new GoogleUserDto
            {
                Id = result.Payload.Subject,
                Email = result.Payload.Email,
                Name = result.Payload.Name,
                PhotoUrl = result.Payload.PhotoUrl
            };

            return new GoogleLoginCommandResponse
            {
                Token = result.Token,
                User = userDto
            };
        }
    }
}
