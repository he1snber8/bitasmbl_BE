using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Repositories.Configurations;

public class ProjectLinksConfiguration : IEntityTypeConfiguration<ProjectLink>
{
    public void Configure(EntityTypeBuilder<ProjectLink> builder)
    {
        builder.HasKey(pi => new { pi.Id, pi.ProjectId, pi.UrlName, pi.UrlValue });

        // builder.HasOne(pi => pi.Project)
        //     .WithMany(p => p.ProjectLinks)
        //     .HasForeignKey(pi => pi.ProjectId)
        //     .IsRequired();

        builder.Property(c => c.UrlName)
            .HasColumnType("nvarchar(128)")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(c => c.UrlValue)
            .HasColumnType("varchar(128)")
            .HasMaxLength(128) // Explicitly set max length for EF Core metadata
            .IsRequired();
    }
}