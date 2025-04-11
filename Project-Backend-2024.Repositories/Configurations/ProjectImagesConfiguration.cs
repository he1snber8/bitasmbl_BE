using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Repositories.Configurations;

public class ProjectImagesConfiguration : IEntityTypeConfiguration<ProjectImage>
{
    public void Configure(EntityTypeBuilder<ProjectImage> builder)
    {
        builder.HasKey(pi => new {pi.ProjectId, pi.ImageUrl});
        
        builder.HasOne(pi => pi.Project)
            .WithMany(p => p.ProjectImages)
            .HasForeignKey(pi => pi.ProjectId);

        builder.Property(c => c.ImageUrl)
            .HasColumnType("varchar(2048)");
    }
}