using PaymentService.Application.DTOs;

namespace PaymentService.Application.Interfaces
{
    public interface IUpdatePaymentUseCase
    {
        Task<bool> ExecuteAsync(Guid paymentId, PaymentUpdateDto dto);
    }
}
