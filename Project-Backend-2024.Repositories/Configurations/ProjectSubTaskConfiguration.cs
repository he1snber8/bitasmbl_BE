// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Project_Backend_2024.DTO;
//
// namespace Project_Backend_2024.Repositories.Configurations;
//
// public class ProjectSubTaskConfiguration : IEntityTypeConfiguration<ProjectSubTask>
// {
//     public void Configure(EntityTypeBuilder<ProjectSubTask> builder)
//     {
//         // builder.HasKey(pi => new {pi.TaskId});
//         
//         builder.HasOne(pi => pi.Task)
//             .WithMany(p => p.ProjectSubTasks)
//             .HasForeignKey(pi => pi.TaskId);
//
//         builder.Property(c => c.Content)
//             .HasColumnType("nvarchar(128)");
//     }
// }