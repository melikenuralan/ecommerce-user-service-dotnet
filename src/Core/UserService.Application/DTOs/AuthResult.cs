using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.DTOs
{
    public class AuthResult
    {
        public bool Succeeded { get; private set; }
        public string? Token { get; private set; }
        public List<string>? Errors { get; private set; }

        public static AuthResult Success(string token) =>
            new AuthResult { Succeeded = true, Token = token };

        public static AuthResult Failure(params string[] errors) =>
            new AuthResult { Succeeded = false, Errors = errors.ToList() };
    }

}
