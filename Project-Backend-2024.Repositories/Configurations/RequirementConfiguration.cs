using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Repositories.Configurations;

public class RequirementConfiguration : IEntityTypeConfiguration<Requirement>
{
    public void Configure(EntityTypeBuilder<Requirement> builder)
    {
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Name)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.HasIndex(r => r.Name)
            .IsUnique();
        
        builder.ToTable("Requirements");
    }
}