using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;

namespace PaymentService.Application.Interfaces
{
    public interface IGetAllPaymentsUseCase
    {
        Task<List<Payment>> ExecuteAsync();

    }
}
