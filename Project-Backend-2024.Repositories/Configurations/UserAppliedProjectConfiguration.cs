using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;
using Project_Backend_2024.DTO.Enums;

namespace Project_Backend_2024.Repositories.Configurations;

internal class UserAppliedProjectConfiguration : IEntityTypeConfiguration<UserAppliedProject>
{
    public void Configure(EntityTypeBuilder<UserAppliedProject> builder)
    {
        builder.HasKey(p => new { p.ProjectId, p.UserId});

        builder.Property(ap => ap.ApplicationStatus)
            .HasConversion(
                v => v.ToString()!,
                v => (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), v))
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .HasDefaultValueSql("'Pending'"); 

        builder.Property(ap => ap.DateApplied)
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");
        
        builder.Property(ap => ap.UserId)
            .HasColumnType("nvarchar")
            .HasMaxLength(450);
        
        builder.HasOne(ap => ap.User)
            .WithMany(a => a.AppliedProjects)
            .HasForeignKey(ap => ap.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(ap => ap.Project)
            .WithMany()
            .HasForeignKey(ap => ap.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}