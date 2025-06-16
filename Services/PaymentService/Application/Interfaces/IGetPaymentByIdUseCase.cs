using PaymentService.Domain.Entities;

namespace PaymentService.Application.Interfaces
{
    public interface IGetPaymentByIdUseCase
    {
        Task<Payment?> ExecuteAsync(Guid id);

    }
}
