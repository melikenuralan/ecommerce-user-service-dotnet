using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Persistence.Data;
using UserService.Persistence.Identity;


namespace UserService.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("UserDbConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new Exception("UserDbConnection environment variable not set.");


            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<UserServiceDbContext>();
        }
    }
}
