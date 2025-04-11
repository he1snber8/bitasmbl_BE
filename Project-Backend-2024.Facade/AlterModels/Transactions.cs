using MediatR;
using Project_Backend_2024.Facade.FetchModels;

namespace Project_Backend_2024.Facade.AlterModels;

public record CreateTransactionCommand(
    decimal Amount,
    string Currency,
    string PaymentMethod,
    string Status,
    string TransactionType,
    string? PaypalTransactionId
) : IRequest<GetTransactionModel>;


public record UpdateTransactionCommand(
    int Id,
    int Status
) : IRequest<GetTransactionModel>;


public record DeleteTransactionCommand(int Id) : IRequest<bool>;