using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;

namespace PaymentService.Application.Interfaces
{
    public interface IGetPaymentStatusUseCase
    {
        Task<List<Payment>> ExecuteAsync(PaymentStatusEnum status);

    }
}
