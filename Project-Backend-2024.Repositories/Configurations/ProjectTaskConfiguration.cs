// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Project_Backend_2024.DTO;
//
// namespace Project_Backend_2024.Repositories.Configurations;
//
// public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
// {
//     public void Configure(EntityTypeBuilder<ProjectTask> builder)
//     {
//         builder.HasKey(pi => new {pi.ProjectId, pi.Id});
//         
//         builder.HasOne(pi => pi.Project)
//             .WithMany(p => p.ProjectTasks)
//             .HasForeignKey(pi => pi.ProjectId);
//
//         builder.Property(c => c.Title)
//             .HasColumnType("nvarchar(64)");
//         
//         builder.Property(p => p.CreatedAt)
//             .HasColumnType("datetime")
//             .HasDefaultValueSql("GETDATE()");
//     }
// }