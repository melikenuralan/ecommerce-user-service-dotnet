using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Common;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Entities
{
    public class UserProfile : BaseEntity
    {
        public FullName FullName { get; private set; }
        public string? Bio { get; private set; }
        public SocialLink? Instagram { get; private set; }
        public SocialLink? Linkedin { get; private set; }
        public LanguagePreference PreferredLanguage { get; private set; }

        private UserProfile() { }

        public UserProfile(FullName fullName, string? bio, LanguagePreference language)
        {
            FullName = fullName;
            Bio = bio;
            PreferredLanguage = language;
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
