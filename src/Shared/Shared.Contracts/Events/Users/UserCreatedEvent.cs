namespace Shared.Contracts.Events.Users
{
    public class UserCreatedEvent
    {
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
