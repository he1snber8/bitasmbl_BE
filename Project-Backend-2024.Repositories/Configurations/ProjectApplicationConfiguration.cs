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

        builder.HasIndex(p => new { p.ProjectId, p.ApplicantId}).IsUnique();

        builder.Property(ap => ap.ApplicationStatus)
            .HasConversion(
                v => v.ToString(),
                v => (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), v!))
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .HasDefaultValueSql("'Pending'"); 

        builder.Property(ap => ap.DateApplied)
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(ap => ap.ApplicantId)
            .HasColumnType("nvarchar")
            .HasMaxLength(450);
        
        builder.Property(ap => ap.PrincipalId)
            .HasColumnType("nvarchar")
            .HasMaxLength(450);

        builder.Property(ap => ap.CoverLetter)
            .HasColumnType("nvarchar")
            .HasMaxLength(450);

        builder.Property(ap => ap.Applicant)
            .HasColumnType("varchar")
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(ap => ap.Applicant)
            .HasColumnType("varchar")
            .HasMaxLength(30)
            .IsRequired(false);

        builder.HasOne(ap => ap.User)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(ap => ap.ApplicantId);

        builder.HasOne(ap => ap.Project)
            .WithMany(a => a.Applications)
            .HasForeignKey(ap => ap.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}