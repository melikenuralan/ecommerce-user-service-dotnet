namespace UserService.Application.DTOs
{
    public class GoogleLoginResultDto
    {
        public TokenDto Token { get; set; }
        public GooglePayload Payload { get; set; }
    }
}
