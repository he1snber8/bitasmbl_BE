using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Repositories.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasColumnType("varchar")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasColumnType("varchar")
            .HasMaxLength(128)
            .IsRequired(false);

        builder.HasIndex(c => c.Name)
            .IsUnique();
    }
}