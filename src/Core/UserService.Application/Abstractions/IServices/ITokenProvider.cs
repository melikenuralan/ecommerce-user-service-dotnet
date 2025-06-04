using UserService.Application.DTOs.Auth;



namespace UserService.Application.Abstractions.IServices
{
    public interface ITokenProvider
    {
        TokenDto GenerateToken(int minute, string userId, string userName, IList<string> roles);
        string CreateRefreshToken();
    }
}
