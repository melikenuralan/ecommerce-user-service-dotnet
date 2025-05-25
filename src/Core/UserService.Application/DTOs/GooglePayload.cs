namespace UserService.Application.DTOs
{
    public class GooglePayload
    {
        public string Provider { get; set; }
        public string Subject { get; set; }  // Google user ID
        public string Email { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
