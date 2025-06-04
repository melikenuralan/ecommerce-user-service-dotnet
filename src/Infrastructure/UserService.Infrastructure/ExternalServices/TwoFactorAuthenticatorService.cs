using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Abstractions.IExternalServices;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs.Identity;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.ExternalServices
{
    public class TwoFactorAuthenticatorService : ITwoFactorAuthenticatorService
    {
        private readonly IUserManagementService _userManagementService;
        private readonly UrlEncoder _urlEncoder;

        public TwoFactorAuthenticatorService(IUserManagementService userManagementService, UrlEncoder urlEncoder)
        {
            _userManagementService = userManagementService;
            _urlEncoder = urlEncoder;
        }

        public async Task<string> GenerateSharedKey(Guid userId)
        {
            string sharedKey = await _userManagementService.GetAuthenticatorKeyAsync(userId);
            if (string.IsNullOrWhiteSpace(sharedKey))
            {
                await _userManagementService.ResetAuthenticatorKeyAsync(userId);
                sharedKey = await _userManagementService.GetAuthenticatorKeyAsync(userId);
            }
            return sharedKey!;
        }
        public async Task<string> GenerateQrCodeUri(string sharedKey, string title, AppUserDto user) =>
        $"otpauth://totp/{_urlEncoder.Encode(title)}:{_urlEncoder.Encode(user.Email)}?secret={sharedKey}&issuer={_urlEncoder.Encode(title)}";
        public async Task<(bool IsVerified, IEnumerable<string>? RecoveryCodes)> VerifyAuthenticatorCodeAsync(Guid userId, string verificationCode)
        {
            var user = await _userManagementService.FindByIdAsync(userId.ToString());
            if (user == null)
                return (false, null);
            bool isValid = await _userManagementService.VerifyAuthenticatorCodeAsync(user , verificationCode);
            if (!isValid)
                return (false, null);

            user.TwoFactorEnabled = true;
            await _userManagementService.UpdateAsync(user);

            var recoveryCodes = await _userManagementService.GenerateNewTwoFactorRecoveryCodesAsync(user, 5);
            return (true, recoveryCodes);
        }

    }
}
