using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Persistence.Identity;

namespace UserService.Persistence.Data
{
    public class UserServiceDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}


/* 
 * isimleri özelleştirmek istersek --> builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<AppRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");*/