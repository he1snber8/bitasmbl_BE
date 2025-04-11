using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_Backend_2024.DTO.Enums;
using Project_Backend_2024.DTO.Interfaces;

namespace Project_Backend_2024.DTO;

    public class Transaction : IEntity
    {
        public int Id { get; set; } // Unique transaction ID
        public string? UserId { get; set; } // Reference to User table
        public decimal Amount { get; set; } // Transaction amount (+ for deposit, - for withdrawal)
        public string? Currency { get; set; } // Currency (e.g., "USD", "GEL")
        public string? PaymentMethod { get; set; } // Payment method (PayPal, Credit Card, etc.)
        public TransactionStatus Status { get; set; } // Enum for Pending, Completed, Failed
        public TransactionType TransactionType { get; set; } // Deposit, Withdrawal, Purchase
        public string? PaypalTransactionId { get; set; } // Stores PayPal transaction reference (if applicable)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for the transaction
        public User User { get; set; }  // Navigation property
    }

   

