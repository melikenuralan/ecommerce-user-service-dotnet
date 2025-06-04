namespace Shared.Contracts.Events.Users
{
    public class PasswordResetEvent
    {
        public string Email { get; set; }
        public string ResetLink { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }

}