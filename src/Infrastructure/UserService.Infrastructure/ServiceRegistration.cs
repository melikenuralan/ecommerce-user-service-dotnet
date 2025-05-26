using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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


            // JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var tokenSettings = configuration.GetSection("Token");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenSettings["Issuer"],
                        ValidAudience = tokenSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings["SecurityKey"]!))
                    };
                });
        }
    }
}
