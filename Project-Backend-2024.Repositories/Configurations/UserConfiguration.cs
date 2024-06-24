﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;

namespace Project_Backend_2024.Repositories.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
           .HasColumnType("varchar")
           .HasMaxLength(30)
           .IsRequired();

            builder.HasIndex(u => u.UserName)
            .IsUnique();

            builder.HasIndex(u => u.Email)
            .IsUnique();

            builder.Property(u => u.PasswordHash)
            .HasColumnType("varbinary(MAX)")
            .IsRequired();

            builder.Property(u => u.IsDeleted)
            .HasDefaultValue(false);

            builder.Property(u => u.Email)
            .HasMaxLength(50)
            .HasColumnType("varchar")
            .IsRequired();

            builder.Property(c => c.Picture)
                .HasColumnType("varbinary(MAX)");

            builder.Property(c => c.Bio)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.DateJoined)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.LastLogin)
                .HasColumnType("datetime");

            builder.HasMany(p => p.Projects)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.PrincipalId);
        }
    }
}
