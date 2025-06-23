namespace UserService.Domain.DomainEvents
{
    public class UserBlockedEvent
    {
        public Guid UserId { get; }
        public DateTime BlockedAt { get; }

        public UserBlockedEvent(Guid userId, DateTime blockedAt)
        {
            UserId = userId;
            BlockedAt = blockedAt;
        }
    }
}