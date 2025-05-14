using UserService.Domain.Common;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Entities
{
    public class UserSettings : BaseEntity
    {
        public ThemePreference Theme { get; private set; }
        public bool IsProfilePublic { get; private set; }
        public bool RecieveNotifications { get; private set; }
        private UserSettings() { }

        public UserSettings(ThemePreference theme, bool isPublic, bool receiveNotifications)
        {
            Theme = theme;
            IsProfilePublic = isPublic;
            RecieveNotifications = receiveNotifications;
        }
        public void ChangeTheme(ThemePreference newTheme)
        {
            Theme = newTheme;
        }
        public void TogglePrivacy() => IsProfilePublic = !IsProfilePublic;
        public void ToggleNotifications() => RecieveNotifications = !RecieveNotifications;
    }
}
