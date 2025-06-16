using PaymentService.Application.DTOs;
using PaymentService.Application.Interfaces;

namespace PaymentService.Application.UseCases
{
    public class UpdatePaymentUseCase : IUpdatePaymentUseCase
    {
        private readonly IPaymentRepository _repository;

        public UpdatePaymentUseCase(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(Guid paymentId, PaymentUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(paymentId);
            if (existing == null)
                return false;

            existing.Amount = dto.Amount;
            existing.Status = dto.NewStatus;
            existing.Notes = dto.Notes;
            existing.ModifiedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(existing);
            return true;
        }
    }
}
