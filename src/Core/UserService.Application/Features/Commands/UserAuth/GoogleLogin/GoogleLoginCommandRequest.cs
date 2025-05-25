using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace UserService.Application.Features.Commands.UserAuth.GoogleLogin
{
    public class GoogleLoginCommandRequest : IRequest<GoogleLoginCommandResponse>
    { 
        public string IdToken { get; set; }
        public string Provider { get; set; }
    }
}
