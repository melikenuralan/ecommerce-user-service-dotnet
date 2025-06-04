using UserService.Domain.Enums;

namespace UserService.Application.DTOs
{
    public class RegisterRequestDto
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public TwoFactorType TwoFactorType { get; set; }
    }
}
