using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Repositories.Configurations;

namespace Project_Backend_2024.Repositories;

public class DatabaseContext(DbContextOptions<DatabaseContext> opts) : IdentityDbContext<User>(opts)
{
    public new DbSet<User> Users { get; set; }
    public DbSet<ProjectApplication> ProjectApplications { get; set; }
    public DbSet<ProjectCategory> ProjectCategories { get; set; }
    public DbSet<ProjectRequirement> ProjectRequirements { get; set; }
    public DbSet<ProjectImage> ProjectImages { get; set; }
    public DbSet<UserAppliedProject> UserAppliedProjects { get; set; }
    public DbSet<UserSocialLink> UserSocialLinks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Requirement> Requirements { get; set; }
    public DbSet<RequirementTest> RequirementsTests { get; set; }
    // public DbSet<ProjectTask> ProjectTasks { get; set; }
    // public DbSet<ProjectSubTask> ProjectSubTasks { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<UserSkills> UserSkills { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new UserAppliedProjectConfiguration());
        modelBuilder.ApplyConfiguration(new UserSocialLinkConfiguration());
        modelBuilder.ApplyConfiguration(new SkillsConfiguration());
        modelBuilder.ApplyConfiguration(new UserSkillsConfiguration());
        modelBuilder.ApplyConfiguration(new RequirementConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectRequirementConfiguration());
        // modelBuilder.ApplyConfiguration(new ProjectTaskConfiguration());
        // modelBuilder.ApplyConfiguration(new ProjectSubTaskConfiguration());
        modelBuilder.ApplyConfiguration(new RequirementTestConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectImagesConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());

        const string adminId = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
        const string roleId = adminId;
        
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = adminId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                NormalizedName = "USER"
            });
        
        var hasher = new PasswordHasher<User>();
        
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = adminId,
            UserName = "admin",
            NormalizedUserName = "admin",
            Email = "lukakhaja@yahoo.com",
            NormalizedEmail = "lukakhaja@yahoo.com".ToUpper(),
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "Th1sIsTh3AdminPassword!23"),
            SecurityStamp = string.Empty
        });
        
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            UserId = adminId,
            RoleId = roleId,
        });
    }
}