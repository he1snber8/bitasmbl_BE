using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Repositories.Configurations;

internal class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(t => t.Id); 

        builder.Property(o => o.Name)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(o => o.Description)
            .HasColumnType("nvarchar")
            .HasMaxLength(1024);

        builder.Property(o => o.Industry)
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.Property(o => o.OrganizationSize)
            .IsRequired();

        builder.Property(o => o.IsVerified)
            .IsRequired();

        builder.Property(o => o.WebsiteUrl)
            .HasColumnType("varchar")
            .HasMaxLength(2048);

        builder.Property(o => o.LogoUrl)
            .HasColumnType("varchar")
            .HasMaxLength(2048);

        builder.Property(o => o.Country)
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(o => o.ContactEmail)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();
    }
}