using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.DTOs
{
    public class GoogleLoginResultDto
    {
        public TokenDto Token { get; set; }
        public GooglePayload Payload { get; set; }
    }
}
