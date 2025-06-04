using UserService.Domain.Enums;

namespace UserService.Application.DTOs.Auth
{
    public class AuthResultDto
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public TokenDto? Token { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public TwoFactorType? TwoFactorType { get; set; }
        public string[]? Errors { get; set; }

        public static AuthResultDto Success(TokenDto token) => new()
        {
            Succeeded = true,
            Token = token,
            Message = "Başarılı giriş"
        };

        public static AuthResultDto Require2FA(TwoFactorType type) => new()
        {
            Succeeded = false,
            RequiresTwoFactor = true,
            TwoFactorType = type,
            Token = null, // ❗ Mutlaka null olmalı
            Message = "İki adımlı doğrulama gerekli."
        };

        public static AuthResultDto Failure(params string[] errors) => new()
        {
            Succeeded = false,
            Errors = errors,
            Message = errors.FirstOrDefault()
        };
    }

}
