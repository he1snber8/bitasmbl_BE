using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Project_Backend_2024.DTO;
using Project_Backend_2024.Facade.AlterModels;
using Project_Backend_2024.Facade.Exceptions;
using Project_Backend_2024.Facade.FetchModels;
using Project_Backend_2024.Facade.Interfaces;

namespace Project_Backend_2024.Services.CommandServices.Transactions;

public class CreateTransaction(
    ITransactionRepository transactionRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreateTransactionCommand, GetTransactionModel>
{
    public async Task<GetTransactionModel> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var userId = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value
                     ?? throw new UserNotFoundException();
        
        var transaction = mapper.Map<Transaction>(request);
        
        transaction.UserId = userId;

        transactionRepository.Insert(transaction);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<GetTransactionModel>(transaction);
    }
}