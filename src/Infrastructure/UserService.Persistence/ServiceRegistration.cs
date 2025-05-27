using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Abstractions.IServices;
using UserService.Domain.Interfaces;
using UserService.Persistence.Concretes.Repositories;
using UserService.Persistence.Concretes.Services;
using UserService.Persistence.Data;
using UserService.Persistence.Identity;


namespace UserService.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration
                                     .GetConnectionString("UserDbConnection")
                                     ?? throw new InvalidOperationException("ConnectionStrings:UserDbConnection tanımlı değil.");

            services.AddDbContext<UserServiceDbContext>(opts =>
                                      opts.UseNpgsql(connectionString));


            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<UserServiceDbContext>()
            .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };
            });






            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserManagementService, UserManagementService>();

        }
    }
}
