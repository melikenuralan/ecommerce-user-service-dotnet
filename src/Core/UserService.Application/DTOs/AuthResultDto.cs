using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.DTOs
{
    public class AuthResultDto
    {
        public bool Succeeded { get; private set; }
        public TokenDto? Token { get; private set; }
        public List<string>? Errors { get; private set; }

        public static AuthResultDto Success(TokenDto token) =>
            new AuthResultDto { Succeeded = true, Token = token };

        public static AuthResultDto Failure(params string[] errors) =>
            new AuthResultDto { Succeeded = false, Errors = errors.ToList() };
    }
}
