using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.DTOs;

namespace UserService.Application.Abstractions.IExternalServices
{
    public interface ITwoFactorAuthenticatorService
    {
        Task<string> GenerateSharedKey(Guid userId);
        Task<string> GenerateQrCodeUri(string sharedKey, string title, AppUserDto user);
        Task<(bool IsVerified, IEnumerable<string>? RecoveryCodes)> VerifyAuthenticatorCodeAsync(Guid userId, string verificationCode);
    }
}