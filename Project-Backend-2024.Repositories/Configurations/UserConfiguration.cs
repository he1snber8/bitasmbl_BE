using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade;

namespace Project_Backend_2024.Repositories.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
           .HasColumnType("varchar")
           .HasMaxLength(30);

            builder.HasIndex(u => u.Username)
            .IsUnique();

            builder.HasIndex(u => u.Email)
            .IsUnique();

            builder.Property(u => u.Password)
            .HasColumnType("varbinary(MAX)")
            .HasConversion(
            p => p.HashData(),
            p => p);

            builder.Property(u => u.IsDeleted)
            .HasDefaultValue(false);

            builder.Property(u => u.Email)
            .HasMaxLength(50)
            .HasColumnType("varchar");

            builder.Property(c => c.DateJoined)
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.LastLogin)
            .HasColumnType("datetime");

            builder.Property(m => m.Bio)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);

            builder.HasMany(p => p.Projects)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.PrincipalID);

            builder.Property(m => m.Picture)
           .HasColumnType("VARBINARY(MAX)");

        }
    }
}
