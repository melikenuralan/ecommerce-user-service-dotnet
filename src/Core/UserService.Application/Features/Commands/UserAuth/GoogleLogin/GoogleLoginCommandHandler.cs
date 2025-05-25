using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // 1) LoginAsync artık GoogleLoginResultDto döndürüyor
            var result = await _googleAuth.LoginAsync(request.IdToken);

            // 2) Kullanıcı bilgilerini result.Payload’dan al
            var userDto = new GoogleUserDto
            {
                Id = result.Payload.Subject,
                Email = result.Payload.Email,
                Name = result.Payload.Name,
                PhotoUrl = result.Payload.PhotoUrl
            };

            // 3) Cevap DTO’nu doldur
            return new GoogleLoginCommandResponse
            {
                Token = result.Token,  // burada TokenDto
                User = userDto
            };
        }
    }
}
