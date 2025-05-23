﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Backend_2024.DTO;
using Project_Backend_2024.DTO.Enums;

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
                .HasColumnType("varchar");

            builder.Property(c => c.ImageUrl)
                .HasColumnType("varchar(2048)");

            builder.Property(c => c.Bio)
                .HasColumnType("varchar(255)");
            
            builder.Property(c => c.Balance)
                .HasColumnType("smallint") // SQL Server does not have "ushort", so "smallint" (16-bit) is used
                .HasDefaultValue(15) // Default value if needed
                .IsRequired(); 

            builder.Property(c => c.DateJoined)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");
            
            builder.Property(p => p.RegistrationType)
                .HasConversion(
                    rt => rt.ToString(),
                    rt => (RegistrationType)Enum.Parse(typeof(RegistrationType), rt))
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .HasDefaultValueSql("'Standard'"); 

            builder.Property(c => c.LastLogin)
                .HasColumnType("datetime");

            builder.HasMany(p => p.Projects)
            .WithOne()
            .HasForeignKey(u => u.PrincipalId);
            
            builder.HasMany(p => p.ProjectApplications)
                .WithOne()
                .HasForeignKey(u => u.PrincipalId);
            
            builder.HasMany(p => p.AppliedProjects)
                .WithOne()
                .HasForeignKey(u => u.UserId);
        }
    }
}
