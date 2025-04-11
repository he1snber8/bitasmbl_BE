using MediatR;

namespace Project_Backend_2024.Facade.FetchModels;

public record GetTransactionModel(
    int Id,
    string UserId,
    decimal Amount,
    string Currency,
    string PaymentMethod,
    string Status,
    string TransactionType,
    string? PaypalTransactionId,
    DateTime CreatedAt
) : IRequest<GetTransactionModel>;