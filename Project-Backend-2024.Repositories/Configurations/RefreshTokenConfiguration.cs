using Project_Backend_2024.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasIndex(t => t.Token)
            .IsUnique();

        builder.Property(a => a.isActive)
        .HasDefaultValue(true);
    }
}
