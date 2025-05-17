using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;
using UserService.Domain;

namespace UserService.Persistence.Concretes.Services
{
    public class AuthService : IAuthService
    {
        public Task<AuthResult> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuthResult> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
