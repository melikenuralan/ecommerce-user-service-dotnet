using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Abstractions.IExternalServices;
using UserService.Application.Abstractions.IServices;
using UserService.Application.DTOs;
using UserService.Domain.Enums;
using UserService.Persistence.Identity;

namespace UserService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
 
    public class TwoAuthenticationController : ControllerBase
    {
        private readonly ITwoFactorAuthenticatorService _twoFactorAuthenticatorService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenProvider _tokenProvider;
        public TwoAuthenticationController(ITwoFactorAuthenticatorService twoFactorAuthenticatorService, UserManager<AppUser> userManager, ITokenProvider tokenProvider)
        {
            _twoFactorAuthenticatorService = twoFactorAuthenticatorService;
            _userManager = userManager;
            _tokenProvider = tokenProvider;
        }
        [HttpGet("authenticatorww")]
        public IActionResult TestAuth()
        {
            var name = User.Identity?.Name;
            var roles = User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
            return Ok(new { name, roles });
        }

        [HttpGet("select")]
        public IActionResult GetTwoFactorOptions()
        {
            var types = Enum.GetValues<TwoFactorType>()
                            .Select(e => new { id = (int)e, name = e.ToString() })
                            .ToList();
            return Ok(types);
        }

        [HttpPost("select")]
        public IActionResult SelectTwoFactorAuthentication([FromBody] TwoFactorTypeSelectDto model)
        {
            return model.TwoFactorType switch
            {
                TwoFactorType.Authenticator => Ok(new { nextStep = "authenticator" }),
                TwoFactorType.SMS => Ok(new { nextStep = "sms" }),
                TwoFactorType.Email => Ok(new { nextStep = "email" }),
                _ => BadRequest("Geçersiz iki faktör doğrulama yöntemi.")
            };
        }

        [HttpGet("authenticator")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> AuthenticatorVerify()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            var sharedKey = await _twoFactorAuthenticatorService.GenerateSharedKey(user.Id);
            var qrCodeUri = await _twoFactorAuthenticatorService.GenerateQrCodeUri(sharedKey, "www.zirvecati.com", new AppUserDto
            {
                Email = user.Email,
                UserName = user.UserName,
                ReferralCode = user.ReferralCode
            });

            return Ok(new AuthenticatorDto
            {
                SharedKey = sharedKey,
                QrCodeUri = qrCodeUri
            });
        }
        [HttpPost("verify-authenticator")]
      
        public async Task<IActionResult> VerifyAuthenticator([FromBody] AuthenticatorVerifyRequest model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            var (isVerified, recoveryCodes) = await _twoFactorAuthenticatorService
                .VerifyAuthenticatorCodeAsync(model.UserId, model.VerificationCode);

            if (!isVerified)
                return BadRequest("Doğrulama başarısız. Kod geçersiz veya süresi dolmuş olabilir.");

            // ✅ Burada token üretiliyor
            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenProvider.GenerateToken(
                60,
                user.Id.ToString(),
                user.UserName,
                roles
            );

            return Ok(new
            {
                message = "İki faktör doğrulama tamamlandı. Giriş başarılı.",
                token = token,
                recoveryCodes = recoveryCodes // ister göster, ister gösterme
            });
        }


        [HttpPost("sms")]
        public IActionResult SMSVerify()
        {
            return Ok("SMS doğrulama işlemi başlatılacak.");
        }

        [HttpPost("email")]
        public IActionResult EmailVerify()
        {
            return Ok("Email doğrulama işlemi başlatılacak.");
        }
    }
}
