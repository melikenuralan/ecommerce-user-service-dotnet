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

            builder.Owned<Email>();
            builder.Owned<FullName>();
            builder.Owned<ThemePreference>();
            builder.Owned<LanguagePreference>();
            builder.Owned<NotificationSettings>();


            builder.Entity<AppUser>().ToTable("AspNetUsers");
            builder.Entity<AppRole>().ToTable("AspNetRoles");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<Guid>>()
                   .ToTable("AspNetUserRoles");

            builder.Entity<UserService.Domain.Entities.User>(user =>
            {
                user.ToTable("Users");
                user.HasKey(u => u.Id);

                user.Property(u => u.IsActive).IsRequired();
                user.Property(u => u.IsEmailVerified).IsRequired();
                user.Property(u => u.IsPhoneVerified).IsRequired();
                user.Property(u => u.CreatedAt).IsRequired();
                user.Property(u => u.LastLoginAt);

                user.OwnsOne(u => u.Email, emailNav =>
                {
                    emailNav.Property(e => e.Value)
                            .HasColumnName("Email")
                            .IsRequired()
                            .HasMaxLength(200);
                });

                user.OwnsOne(u => u.FullName, nameNav =>
                {
                    nameNav.Property(n => n.FirstName)
                           .HasColumnName("FirstName")
                           .IsRequired()
                           .HasMaxLength(100);
                    nameNav.Property(n => n.LastName)
                           .HasColumnName("LastName")
                           .IsRequired()
                           .HasMaxLength(100);
                });



               

                user.HasMany(u => u.BlockedUsers)
                    .WithOne()                    
                    .HasForeignKey(b => b.UserId);
            });

            builder.Entity<UserProfile>(profile =>
            {
                profile.ToTable("UserProfiles");
                profile.HasKey(p => p.Id);

                profile.HasOne<User>()       
                       .WithOne()     
                       .HasForeignKey<UserProfile>(p => p.Id);
            });
            builder.Entity<UserProfile>(profile =>
            {
                profile.ToTable("UserProfiles");

                // PK = FK: User.Id ile birebir
                profile.HasKey(p => p.Id);
                profile.HasOne<User>()      // UserProfile içinde nav prop tanımlamadıysanız bu şekilde
                       .WithOne(u => u.Profile)
                       .HasForeignKey<UserProfile>(p => p.Id);

                // FullName VO artık FirstName / LastName sahip
                profile.OwnsOne(p => p.FullName, nameNav =>
                {
                    nameNav.Property(n => n.FirstName)
                           .HasColumnName("FirstName")
                           .IsRequired()
                           .HasMaxLength(100);

                    nameNav.Property(n => n.LastName)
                           .HasColumnName("LastName")
                           .IsRequired()
                           .HasMaxLength(100);
                });

                // Instagram VO
                profile.OwnsOne(p => p.Instagram, igNav =>
                {
                    igNav.Property(x => x.Platform)
                         .HasColumnName("InstagramPlatform")
                         .HasMaxLength(50);

                    igNav.Property(x => x.Url)
                         .HasColumnName("InstagramUrl")
                         .HasMaxLength(200);
                });

                // LinkedIn VO
                profile.OwnsOne(p => p.Linkedin, lnNav =>
                {
                    lnNav.Property(x => x.Platform)
                         .HasColumnName("LinkedInPlatform")
                         .HasMaxLength(50);

                    lnNav.Property(x => x.Url)
                         .HasColumnName("LinkedInUrl")
                         .HasMaxLength(200);
                });

                // Bio düz metin, konvansiyonla zaten map edilir, ama istersen explicit:
                profile.Property(p => p.Bio)
                       .HasColumnName("Bio")
                       .HasMaxLength(1000);
            });

            builder.Entity<UserSettings>(settings =>
            {
                settings.ToTable("UserSettings");
                settings.HasKey(s => s.Id);
                settings.OwnsOne(s => s.Theme, themeNav =>
                {
                    themeNav.Property(t => t.Value)
                            .HasColumnName("Theme")
                            .IsRequired()
                            .HasMaxLength(20);
                });

             

                settings.OwnsOne(u => u.Language, langNav =>
                {
                    langNav.Property(l => l.Code)
                           .HasColumnName("LanguageCode")
                           .IsRequired()
                           .HasMaxLength(5);

                    langNav.Property(l => l.Name)
                           .HasColumnName("LanguageName")
                           .IsRequired()
                           .HasMaxLength(50);
                });


                settings.OwnsOne(u => u.NotificationSettings, notifNav =>
                {
                    notifNav.Property(n => n.ReceivePromotions)
                            .HasColumnName("ReceivePromotions")
                            .IsRequired();

                    notifNav.Property(n => n.ReceiveOrderUpdates)
                            .HasColumnName("ReceiveOrderUpdates")
                            .IsRequired();

                    notifNav.Property(n => n.ReceiveSecurityAlerts)
                            .HasColumnName("ReceiveSecurityAlerts")
                            .IsRequired();
                });

            });

            builder.Entity<BlockedUser>(blocked =>
            {
                blocked.ToTable("BlockedUsers");
                blocked.HasKey(b => b.Id);
                blocked.Property(b => b.BlockedAt)
                       .HasDefaultValueSql("NOW()");
            });
        }
    }
}