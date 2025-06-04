namespace UserService.Application.DTOs.TwoFactor
{
    public class AuthenticatorDto
    {
        public string SharedKey { get; set; }
        public string QrCodeUri { get; set; }
    }
}
