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


            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<UserServiceDbContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
        }
    }
}
