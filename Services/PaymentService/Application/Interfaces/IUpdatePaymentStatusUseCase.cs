using PaymentService.Domain.Enums;

namespace PaymentService.Application.Interfaces
{
    public interface IUpdatePaymentStatusUseCase
    {
        Task<bool> ExecuteAsync(Guid id, PaymentStatusEnum status);

    }
}
