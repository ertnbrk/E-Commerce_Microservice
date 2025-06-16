using PaymentService.Application.DTOs;

namespace PaymentService.Application.Interfaces
{
    public interface ICreatePaymentUseCase
    {
        Task<Guid> ExecuteAsync(PaymentCreateDto dto);

    }
}
