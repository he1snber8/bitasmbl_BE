using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Repositories.Configurations;

internal class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        builder.HasKey(tm => tm.Id);

        builder.Property(tm => tm.FirstName)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(tm => tm.LastName)
            .HasColumnType("nvarchar")
            .HasMaxLength(50);

        builder.Property(tm => tm.Email)
            .HasColumnType("varchar")
            .HasMaxLength(128);
        
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(tm => tm.Bio)
            .HasColumnType("nvarchar")
            .HasMaxLength(512);

        builder.Property(tm => tm.Role)
            .HasColumnType("varchar")
            .HasMaxLength(128); 

        builder.Property(tm => tm.ResumeUrl)
            .HasMaxLength(2048)
            .HasColumnType("varchar");
        
        builder.Property(tm => tm.LinkedInUrl)
            .HasMaxLength(2048)
            .HasColumnType("varchar");
        
        builder.HasOne(tm => tm.Team)
            .WithMany(team => team.Members)
            .HasForeignKey(team => team.TeamId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}