using UserService.Domain.Common;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {

        public Email Email { get; private set; }
        public FullName FullName { get; private set; }

        public ThemePreference Theme { get; private set; }
        public LanguagePreference Language { get; private set; }
        public NotificationSettings NotificationSettings { get; private set; }

        public bool IsEmailVerified { get; private set; }
        public bool IsPhoneVerified { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLoginAt { get; private set; }

        private readonly List<BlockedUser> _blockedUsers = new();
        public IReadOnlyCollection<BlockedUser> BlockedUsers => _blockedUsers.AsReadOnly();

        private User() { }

        public User(Guid id , Email email, FullName fullName)
        {
            Id = id;
            Email = email;
            FullName = fullName;
            Theme = ThemePreference.System;
            Language = LanguagePreference.TR;
            NotificationSettings = NotificationSettings.Default();

            IsActive = true;
            IsEmailVerified = false;
            IsPhoneVerified = false;
            CreatedAt = DateTime.UtcNow;
        }

        // === Domain Davranışları ===

        public void ChangeFullName(FullName newName)
        {
            if (newName is null)
                throw new ArgumentNullException(nameof(newName));

            FullName = newName;
        }

        public void UpdatePreferences(ThemePreference theme, LanguagePreference language)
        {
            Theme = theme;
            Language = language;
        }

        public void UpdateNotificationSettings(NotificationSettings settings)
        {
            NotificationSettings = settings;
        }

        public void VerifyEmail() => IsEmailVerified = true;
        public void VerifyPhone() => IsPhoneVerified = true;

        public void Deactivate() => IsActive = false;
        public void Reactivate() => IsActive = true;

        public void UpdateLastLogin() => LastLoginAt = DateTime.UtcNow;

        public void BlockUser(Guid blockedUserId)
        {
            if (_blockedUsers.Any(x => x.BlockedUserId == blockedUserId))
                return;

            _blockedUsers.Add(new BlockedUser(this.Id, blockedUserId));
        }
    }
}
