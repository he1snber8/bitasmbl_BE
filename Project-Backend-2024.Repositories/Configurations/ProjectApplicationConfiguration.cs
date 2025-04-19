using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;
using Project_Backend_2024.DTO.Enums;

namespace Project_Backend_2024.Repositories.Configurations;

public class ProjectApplicationConfiguration : IEntityTypeConfiguration<ProjectApplication>
{
    public void Configure(EntityTypeBuilder<ProjectApplication> builder)
    {
        builder.HasKey(ap => ap.Id);

        builder.HasIndex(p => new { p.ProjectId }).IsUnique();

        builder.Property(ap => ap.Status)
            .HasConversion(
                v => v.ToString()!,
                v => (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), v))
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .HasDefaultValueSql("'Pending'"); 

        builder.Property(ap => ap.DateApplied)
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(ap => ap.CoverLetter)
            .HasColumnType("nvarchar")
            .HasMaxLength(450);
        
        builder.HasOne(ap => ap.Team)
            .WithMany()
            .HasForeignKey(ap => ap.TeamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ap => ap.Project)
            .WithMany(a => a.ProjectApplications)
            .HasForeignKey(ap => ap.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}