using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;
using UserService.Domain.Entities;


namespace UserService.Infrastructure.Services
{
    public class TokenProvider : ITokenProvider
    {
        readonly IConfiguration _configuration;

        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenDto GenerateToken(int minute, string userId, string userName, IList<string> roles)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Token:SecurityKey"]);

            var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.NameIdentifier, userId),
                                    new Claim(ClaimTypes.Name, userName)
                                };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(minute),
                Issuer = _configuration["Token:Issuer"],
                Audience = _configuration["Token:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return new TokenDto
            {
                AccessToken = handler.WriteToken(token),
                Expiration = tokenDescriptor.Expires!.Value,
                RefreshToken = CreateRefreshToken()
            };
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
