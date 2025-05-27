using Microsoft.AspNetCore.Identity;
using UserService.Domain.Enums;

namespace UserService.Persistence.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? ReferralCode { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsPhoneVerified { get; set; }
        public TwoFactorType TwoFactorType { get; set; }
    }
}
