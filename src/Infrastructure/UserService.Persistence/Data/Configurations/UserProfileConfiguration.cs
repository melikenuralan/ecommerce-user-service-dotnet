using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Persistence.Data.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfiles");
            builder.HasKey(p => p.Id);

            // 1-to-1: User.Id ↔ UserProfile.Id
            builder.HasOne<User>()
                   .WithOne(u => u.Profile)
                   .HasForeignKey<UserProfile>(p => p.Id);


            // VO: Instagram
            builder.OwnsOne(p => p.Instagram, ig =>
            {
                ig.Property(x => x.Platform)
                  .HasColumnName("InstagramPlatform")
                  .HasMaxLength(50);
                ig.Property(x => x.Url)
                  .HasColumnName("InstagramUrl")
                  .HasMaxLength(200);
            });

            // VO: LinkedIn
            builder.OwnsOne(p => p.Linkedin, ln =>
            {
                ln.Property(x => x.Platform)
                  .HasColumnName("LinkedInPlatform")
                  .HasMaxLength(50);
                ln.Property(x => x.Url)
                  .HasColumnName("LinkedInUrl")
                  .HasMaxLength(200);
            });

            // Basit property: Bio
            builder.Property(p => p.Bio)
                   .HasColumnName("Bio")
                   .HasMaxLength(1000);
        }
    }
}