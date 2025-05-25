namespace UserService.Application.DTOs
{
    public class AppUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        //    public IList<string> Roles { get; set; }
        public string ReferralCode { get; set; }
    }
}
