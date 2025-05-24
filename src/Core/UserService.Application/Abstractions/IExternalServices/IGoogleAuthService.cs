using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTOs;

namespace UserService.Application.Abstractions.IExternalServices
{
    public interface IGoogleAuthService
    {
        Task<GooglePayload> ValidateAsync(string idToken);
    }
}
