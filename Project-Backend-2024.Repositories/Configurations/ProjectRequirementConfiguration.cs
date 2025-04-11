using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Repositories.Configurations;

public class ProjectRequirementConfiguration : IEntityTypeConfiguration<ProjectRequirement>
{
    public void Configure(EntityTypeBuilder<ProjectRequirement> builder)
    {
        builder.HasKey(pr => new { pr.ProjectId, pr.RequirementId });
        
        builder.HasOne(pr => pr.Project)
            .WithMany(p => p.ProjectRequirements)
            .HasForeignKey(pr => pr.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pr => pr.Requirement)
            .WithMany()
            .HasForeignKey(pr => pr.RequirementId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(p => p.MaxApplicationLimit)
            .HasColumnType("tinyint")
            .HasDefaultValue(0);
        
        builder.Property(p => p.CurrentApplications)
            .HasColumnType("tinyint")
            .HasDefaultValue(0);

        builder.ToTable("ProjectRequirements");
    }
}