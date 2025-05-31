using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.Abstractions;
using UserService.Application.Abstractions.IExternalServices;
using UserService.Application.Abstractions.IServices;
using UserService.Infrastructure.ExternalServices;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddSingleton<ILogService, LogService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddScoped<ITwoFactorAuthenticatorService, TwoFactorAuthenticatorService>();
            services.AddHttpClient<ICaptchaService,CaptchaService>();

            services.AddAuthentication("Admin")
                .AddJwtBearer("Admin", options =>
                {
                    var tokenSettings = configuration.GetSection("Token");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = tokenSettings["Audience"],
                        ValidIssuer = tokenSettings["Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings["SecurityKey"]!)),
                        NameClaimType = ClaimTypes.Name,
                        RoleClaimType = ClaimTypes.Role
                    };
                });

        }
    }
}
