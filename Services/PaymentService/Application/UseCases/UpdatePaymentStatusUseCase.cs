using PaymentService.Application.Interfaces;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;

namespace PaymentService.Application.UseCases
{
    public class UpdatePaymentStatusUseCase : IUpdatePaymentStatusUseCase
    {
        private readonly IPaymentRepository _repository;

        public UpdatePaymentStatusUseCase(IPaymentRepository repository)
        {
            _repository = repository;
        }

       

        public async Task<bool> ExecuteAsync(Guid id, PaymentStatusEnum newStatus)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null)
                return false;

            payment.Status = newStatus;
            payment.ModifiedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(payment);
            return true;
        }
    }
}
