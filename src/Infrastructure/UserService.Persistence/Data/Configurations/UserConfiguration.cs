using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Persistence.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Tablo ve PK
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            // Basit property’ler
            builder.Property(u => u.IsActive)
                   .IsRequired();
            builder.Property(u => u.IsEmailVerified)
                   .IsRequired();
            builder.Property(u => u.IsPhoneVerified)
                   .IsRequired();
            builder.Property(u => u.CreatedAt)
                   .IsRequired();
            builder.Property(u => u.LastLoginAt);

            // VO: Email
            builder.OwnsOne(u => u.Email, emailNav =>
            {
                emailNav.Property(e => e.Value)
                        .HasColumnName("Email")
                        .IsRequired()
                        .HasMaxLength(200);
            });

            // VO: FullName
            builder.OwnsOne(u => u.FullName, nameNav =>
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

            // İlişkiler
            builder.HasMany(u => u.BlockedUsers)
                   .WithOne()
                   .HasForeignKey(b => b.UserId);
        }
    }
}