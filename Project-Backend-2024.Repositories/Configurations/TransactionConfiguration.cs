// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Project_Backend_2024.DTO;
// using Project_Backend_2024.DTO.Enums;
//
// namespace Project_Backend_2024.Repositories.Configurations;
//
// public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
// {
//     public void Configure(EntityTypeBuilder<Transaction> builder)
//     {
//         builder.HasKey(t => t.Id);
//
//         builder.Property(t => t.Amount)
//             .HasColumnType("decimal(10,2)")
//             .IsRequired();
//
//         builder.Property(t => t.Currency)
//             .HasColumnType("varchar(3)")
//             .HasMaxLength(3)
//             .IsRequired();
//
//         builder.Property(t => t.PaymentMethod)
//             .HasColumnType("varchar(50)")
//             .HasMaxLength(50)
//             .IsRequired();
//
//         builder.Property(t => t.Status)
//             .HasConversion(
//                 v => v.ToString(),
//                 v => (TransactionStatus)Enum.Parse(typeof(TransactionStatus), v))
//             .IsRequired();
//
//         builder.Property(t => t.TransactionType)
//             .HasConversion(
//                 v => v.ToString(),
//                 v => (TransactionType)Enum.Parse(typeof(TransactionType), v))
//             .IsRequired();
//
//         builder.Property(t => t.PaypalTransactionId)
//             .HasColumnType("varchar(255)")
//             .HasMaxLength(255)
//             .IsRequired(false);
//
//         builder.Property(t => t.CreatedAt)
//             .HasColumnType("datetime")
//             .IsRequired();
//
//         builder.HasIndex(t => t.PaypalTransactionId)
//             .IsUnique(false); // Not unique because multiple transactions might not have this field
//
//         builder.HasOne(t => t.User)
//             .WithMany(u => u.Transactions)
//             .HasForeignKey(t => t.UserId)
//             .OnDelete(DeleteBehavior.Cascade);
//     }
// }