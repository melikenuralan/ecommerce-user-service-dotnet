using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Persistence.Data.Configurations
{
    public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
    {
        public void Configure(EntityTypeBuilder<UserSettings> builder)
        {
            builder.ToTable("UserSettings");
            builder.HasKey(s => s.Id);

            // VO: ThemePreference
            builder.OwnsOne(s => s.Theme, theme =>
            {
                theme.Property(t => t.Value)
                     .HasColumnName("Theme")
                     .IsRequired()
                     .HasMaxLength(20);
            });

            // VO: LanguagePreference
            builder.OwnsOne(s => s.Language, lang =>
            {
                lang.Property(l => l.Code)
                    .HasColumnName("LanguageCode")
                    .IsRequired()
                    .HasMaxLength(5);
                lang.Property(l => l.Name)
                    .HasColumnName("LanguageName")
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // VO: NotificationSettings
            builder.OwnsOne(s => s.NotificationSettings, notif =>
            {
                notif.Property(n => n.ReceivePromotions)
                     .HasColumnName("ReceivePromotions")
                     .IsRequired();
                notif.Property(n => n.ReceiveOrderUpdates)
                     .HasColumnName("ReceiveOrderUpdates")
                     .IsRequired();
                notif.Property(n => n.ReceiveSecurityAlerts)
                     .HasColumnName("ReceiveSecurityAlerts")
                     .IsRequired();
            });
        }
    }
}
