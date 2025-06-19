using UserService.Domain.Common;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Entities
{
    public class UserProfile : BaseEntity
    {
        public string? Bio { get; private set; }
        public SocialLink? Instagram { get; private set; }
        public SocialLink? Linkedin { get; private set; }
        private UserProfile() { }

        public UserProfile(Guid id, string? bio)
        {
            Id = id;
            Bio = bio;
        }
        public void SetSocialLinks(SocialLink? instagram, SocialLink? linkedin)
        {
            Instagram = instagram;
            Linkedin = linkedin;
        }
        public void UpdateBio(string? bio)
        {
            Bio = bio;
        }
    }
}
