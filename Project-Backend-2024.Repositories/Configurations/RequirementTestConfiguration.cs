// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Project_Backend_2024.DTO;
//
// namespace Project_Backend_2024.Repositories.Configurations;
//
// public class RequirementTestConfiguration : IEntityTypeConfiguration<RequirementTest>
// {
//     public void Configure(EntityTypeBuilder<RequirementTest> builder)
//     {
//         builder.HasKey(r => r.Id);
//         
//         builder.HasIndex(r => new {r.Id, r.RequirementId}).IsUnique();
//
//         builder.Property(r => r.Question)
//             .HasColumnType("nvarchar")
//             .HasMaxLength(256)
//             .IsRequired();
//
//         builder.Property(r => r.CorrectAnswer)
//             .HasColumnType("nvarchar")
//             .HasMaxLength(256)
//             .IsRequired();
//         
//         builder.Property(r => r.Answers)
//             .HasColumnType("nvarchar(512)")
//             .HasConversion(
//                 v => string.Join(";", v),  // Convert List<string> to string for storage
//                 v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to List<string>
//             );
//         
//         builder.HasOne(pr => pr.Requirement)
//             .WithMany(rt => rt.RequirementTests)
//             .HasForeignKey(rt => rt.RequirementId)
//             .OnDelete(DeleteBehavior.NoAction);
//
//         builder.ToTable("RequirementTests");
//     }
//
// }