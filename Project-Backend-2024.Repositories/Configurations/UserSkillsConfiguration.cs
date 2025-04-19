// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Project_Backend_2024.DTO;
//
// namespace Project_Backend_2024.Repositories.Configurations;
//
// public class UserSkillsConfiguration : IEntityTypeConfiguration<UserSkills>
// {
//     public void Configure(EntityTypeBuilder<UserSkills> builder)
//     {
//         builder.HasKey(us => new { us.UserId, us.SkillId });
//
//         // builder.HasOne(ap => ap.User)
//         //     .WithMany(w => w.UserSkills)
//         //     .OnDelete(DeleteBehavior.NoAction)
//         //     .HasForeignKey(ap => ap.UserId)
//         //     .IsRequired();
//         
//         builder.Property(ap => ap.UserId)
//             .HasColumnType("nvarchar")
//             .HasMaxLength(450);
//         
//         builder.HasOne(ap => ap.)
//             .WithMany(s => s.UserSkills)
//             .HasForeignKey(ap => ap.SkillId)
//             .IsRequired();
//
//     }
//
// }