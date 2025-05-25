using UserService.Domain.Common;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Entities
{
    public class UserSettings : BaseEntity
    {
        // FK to User
        public Guid UserId { get; private set; }
        public User User { get; private set; }

        // Value Objects
        public ThemePreference Theme { get; private set; }
        public LanguagePreference Language { get; private set; }
        public NotificationSettings NotificationSettings { get; private set; }

        // Simple flags
        public bool IsProfilePublic { get; private set; }
        public bool ReceiveNotifications { get; private set; }

        private UserSettings() { /* EF */ }

        public UserSettings(
            Guid userId,
            ThemePreference theme,
            LanguagePreference language,
            NotificationSettings notificationSettings,
            bool isProfilePublic,
            bool receiveNotifications)
        {
            UserId = userId;
            Theme = theme;
            Language = language;
            NotificationSettings = notificationSettings;
            IsProfilePublic = isProfilePublic;
            ReceiveNotifications = receiveNotifications;
        }

        public static UserSettings CreateDefault(Guid userId) =>
            new UserSettings(
                userId,
                ThemePreference.System,
                LanguagePreference.TR,
                NotificationSettings.Default(),
                isProfilePublic: true,
                receiveNotifications: true
            );

        // Domain behaviors
        public void ChangeTheme(ThemePreference newTheme) =>
            Theme = newTheme;

        public void ChangeLanguage(LanguagePreference newLanguage) =>
            Language = newLanguage;

        public void UpdateNotificationSettings(NotificationSettings settings) =>
            NotificationSettings = settings;

        public void TogglePrivacy() =>
            IsProfilePublic = !IsProfilePublic;

        public void ToggleNotifications() =>
            ReceiveNotifications = !ReceiveNotifications;
    }
}
