using Project_Backend_2024.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserSkillsConfiguration : IEntityTypeConfiguration<UserSkills>
{
    public void Configure(EntityTypeBuilder<UserSkills> builder)
    {
        builder.HasNoKey();

        builder.HasIndex(us => new {us.UserID, us.SkillID }).IsUnique();

        builder.HasOne(ap => ap.User)
           .WithMany()
           .OnDelete(DeleteBehavior.NoAction)
           .HasForeignKey(ap => ap.UserID)
           .IsRequired();
           
        builder.HasOne(ap => ap.Skill)
            .WithMany()
            .HasForeignKey(ap => ap.SkillID)
            .IsRequired();

    }

}
