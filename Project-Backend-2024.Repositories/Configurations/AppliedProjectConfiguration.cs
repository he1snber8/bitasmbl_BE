using Project_Backend_2024.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class AppliedProjectConfiguration : IEntityTypeConfiguration<AppliedProject>
{
    public void Configure(EntityTypeBuilder<AppliedProject> builder)
    {
        builder.HasNoKey();

        builder.HasIndex(p => new { p.ProjectID, p.UserID }).IsUnique();

        builder.Property(ap => ap.ApplicationStatus)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .HasDefaultValueSql("'Active'");

        builder.Property(ap => ap.DateApplied)
            .HasColumnType("datetime");

        builder.Property(ap => ap.CoverLetter)
            .HasColumnType("nvarchar")
            .HasMaxLength(255)
            .IsRequired(false);

        builder.HasOne(ap => ap.User)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(ap => ap.UserID)
            .IsRequired();

        builder.HasOne(ap => ap.Project)
            .WithMany()
            .HasForeignKey(ap => ap.ProjectID)
            .IsRequired();

    }
}
