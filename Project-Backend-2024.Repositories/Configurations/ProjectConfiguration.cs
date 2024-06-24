using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;
using Project_Backend_2024.DTO.Enums;

namespace Project_Backend_2024.Repositories.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(ap => ap.PrincipalId)
            .HasColumnType("nvarchar")
            .HasMaxLength(450);

        builder.HasIndex(p => p.Name)
            .IsUnique();

        builder.Property(p => p.Description)
            .HasColumnType("nvarchar")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasConversion(
                v => v.ToString(),
                v => (ProjectStatus)Enum.Parse(typeof(ProjectStatus), v))
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .HasDefaultValueSql("'Active'"); 

        builder.Property(p => p.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(p => p.DateCreated)
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne(p => p.User)
            .WithMany(user => user.Projects)
            .HasForeignKey(p => p.PrincipalId)
            .IsRequired();
        
        builder.HasMany(p => p.Applications)
            .WithOne(pa => pa.Project)
            .HasForeignKey(pa => pa.ProjectId);
    }
}