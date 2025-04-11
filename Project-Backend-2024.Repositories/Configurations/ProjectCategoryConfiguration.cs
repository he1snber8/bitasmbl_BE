using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Repositories.Configurations;

public class ProjectCategoryConfiguration : IEntityTypeConfiguration<ProjectCategory>
{
    public void Configure(EntityTypeBuilder<ProjectCategory> builder)
    {
        builder.HasKey(pr => new { pr.ProjectId, pr.CategoryId });
        
        builder.HasOne(pr => pr.Project)
            .WithMany(p => p.ProjectCategories)
            .HasForeignKey(pr => pr.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pr => pr.Category)
            .WithMany()
            .HasForeignKey(pr => pr.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("ProjectCategories");
    }
}