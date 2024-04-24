using Project_Backend_2024.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(p => p.Name)
           .IsUnique();

        builder.Property(p => p.Description)
            .HasColumnType("nvarchar")
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(p => p.Status)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .HasDefaultValueSql("'Active'");

        builder.Property(p => p.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(p => p.DateCreated)
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne(p => p.User)
            .WithMany(user => user.Projects)
            .HasForeignKey(p => p.PrincipalID)
            .IsRequired();
    }
}