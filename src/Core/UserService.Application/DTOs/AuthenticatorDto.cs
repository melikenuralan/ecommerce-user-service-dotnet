namespace UserService.Application.DTOs
{
    public class AuthenticatorDto
    {
        public string SharedKey { get; set; }
        public string QrCodeUri { get; set; }
    }
}
