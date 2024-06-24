using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Repositories.Configurations;

public class UserSkillsConfiguration : IEntityTypeConfiguration<UserSkills>
{
    public void Configure(EntityTypeBuilder<UserSkills> builder)
    {
        builder.HasNoKey();

        builder.HasIndex(us => new { UserID = us.UserId, SkillID = us.SkillId }).IsUnique();

        builder.HasOne(ap => ap.User)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(ap => ap.UserId)
            .IsRequired();
        
        builder.Property(ap => ap.UserId)
            .HasColumnType("nvarchar")
            .HasMaxLength(450);
        
        builder.HasOne(ap => ap.Skill)
            .WithMany()
            .HasForeignKey(ap => ap.SkillId)
            .IsRequired();

    }

}