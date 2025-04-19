using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Repositories.Configurations;

internal class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(t => t.ContactEmail)
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(t => t.Location)
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(t => t.YearsInOperation)
            .IsRequired();

        builder.Property(t => t.CompanyProfileUrl)
            .HasColumnType("varchar")
            .HasMaxLength(2048); // generous for URLs
        
        // builder
        //     .HasOne(t => t.TeamManager)
        //     .WithOne(tm => tm.Team)
        //     .HasForeignKey<Team>(t => t.TeamManagerId)
        //     .OnDelete(DeleteBehavior.Restrict);

        builder.Property(t => t.LogoUrl)
            .HasMaxLength(2048)
            .HasColumnType("varchar");
        
        builder.Property(t => t.ContactName)
            .HasMaxLength(50)
            .HasColumnType("varchar");

        builder.HasMany(team => team.Members)
            .WithOne(member => member.Team)
            .HasForeignKey(t => t.TeamId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}