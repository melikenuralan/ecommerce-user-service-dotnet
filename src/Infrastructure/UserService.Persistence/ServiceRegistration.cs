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
            services.AddDbContext<UserServiceDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("UserServiceCollection")));

            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<UserServiceDbContext>();
        }
    }
}
