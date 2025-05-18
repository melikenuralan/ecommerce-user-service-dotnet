using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTOs;
using UserService.Domain.Entities;



namespace UserService.Application.Abstractions.IServices
{
    public interface ITokenProvider
    {
        TokenDto GenerateToken(int minute, string userId, string userName, IList<string> roles);
        string CreateRefreshToken();
    }
}
