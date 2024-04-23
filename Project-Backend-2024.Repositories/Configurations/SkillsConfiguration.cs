using Project_Backend_2024.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SkillsConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(p => p.Name)
           .HasColumnType("varchar")
           .HasMaxLength(50)
           .IsRequired();

        builder.HasIndex(u => u.Name)
            .IsUnique();
    }

}
