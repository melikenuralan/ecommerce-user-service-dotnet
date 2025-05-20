using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UserService.Persistence.Data
{
    public class UserServiceDbContextFactory
        : IDesignTimeDbContextFactory<UserServiceDbContext>
    {
        public UserServiceDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connStr = Environment.GetEnvironmentVariable("UserDbConnection")
                          ?? config.GetConnectionString("UserDbConnection")
                          ?? throw new InvalidOperationException("Connection string bulunamadı.");

            var optionsBuilder = new DbContextOptionsBuilder<UserServiceDbContext>();
            optionsBuilder.UseNpgsql(connStr, sql =>
                sql.MigrationsAssembly(typeof(UserServiceDbContext).Assembly.FullName));

            return new UserServiceDbContext(optionsBuilder.Options);
        }
    }
}
