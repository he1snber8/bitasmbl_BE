using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Repositories.Configurations;

public class UserSocialLinkConfiguration : IEntityTypeConfiguration<UserSocialLink>
{
    public void Configure(EntityTypeBuilder<UserSocialLink> builder)
    {
        builder.HasKey(pi => new { pi.Id }); // Composite key
        builder.HasIndex(pi => new { pi.Id, pi.UserId, pi.SocialUrl }).IsUnique();

        builder.HasOne(pi => pi.User)
            .WithMany(u => u.UserSocials)
            .HasForeignKey(pi => pi.UserId);

        builder.Property(c => c.SocialUrl)
            .HasColumnType("varchar")
            .HasMaxLength(256)
            .IsRequired();
    }
}