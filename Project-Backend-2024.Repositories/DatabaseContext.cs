using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Repositories.Configurations;

namespace Project_Backend_2024.Repositories;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> opts) : base(opts) { }

    public DbSet<User> Users { get; set; }
    public DbSet<AppliedProject> AppliedProjects { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<UserSkills> UserSkills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new AppliedProjectConfiguration());
        modelBuilder.ApplyConfiguration(new SkillsConfiguration());
        modelBuilder.ApplyConfiguration(new UserSkillsConfiguration());
    }

}
