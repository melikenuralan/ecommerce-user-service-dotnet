using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.DTOs.TwoFactor
{
    public class AuthenticatorVerifyRequest
    {
        public Guid UserId { get; set; }
        public string VerificationCode { get; set; }
    }

}
