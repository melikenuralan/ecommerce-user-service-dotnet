using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.ValueObjects;
using UserService.Persistence.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserService.Persistence.Data
{
    public class UserServiceDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options)
        {
        }
        public DbSet<User> DomainUsers { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<BlockedUser> BlockedUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(UserServiceDbContext).Assembly);
        }
    }
}