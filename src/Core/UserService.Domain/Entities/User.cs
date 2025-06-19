using UserService.Domain.Common;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {
        public Email Email { get; private set; }
        public FullName FullName { get; private set; }
        public bool IsEmailVerified { get; private set; }
        public bool IsPhoneVerified { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLoginAt { get; private set; }
        public UserProfile Profile { get; private set; }
        public UserSettings Settings { get; private set; }


        private readonly List<BlockedUser> _blockedUsers = new();
        public IReadOnlyCollection<BlockedUser> BlockedUsers => _blockedUsers.AsReadOnly();

        private User() { }

        public User(Guid id, Email email, FullName fullName)
        {
            Id = id;
            Email = email;
            FullName = fullName;

            IsActive = true;
            IsEmailVerified = false;
            IsPhoneVerified = false;
            CreatedAt = DateTime.UtcNow;

            Profile = new UserProfile(id,bio: null);
            Settings = UserSettings.CreateDefault(id);
        }

        public void ChangeFullName(FullName newName)
        {
            if (newName is null)
                throw new ArgumentNullException(nameof(newName));

            FullName = newName;
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
