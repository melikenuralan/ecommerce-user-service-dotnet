using UserService.Domain.Enums;

namespace UserService.Application.DTOs.Identity
{
    public class AppUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        //    public IList<string> Roles { get; set; }
        public string ReferralCode { get; set; }
        public TwoFactorType TwoFactorType { get; set; }
        public bool TwoFactorEnabled { get; set; }

    }
}
