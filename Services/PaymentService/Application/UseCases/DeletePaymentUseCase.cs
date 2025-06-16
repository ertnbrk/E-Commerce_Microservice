using PaymentService.Application.Interfaces;

namespace PaymentService.Application.UseCases
{
    public class DeletePaymentUseCase : IDeletePaymentUseCase
    {
        private readonly IPaymentRepository _repository;

        public DeletePaymentUseCase(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(Guid paymentId)
        {
            var existing = await _repository.GetByIdAsync(paymentId);
            if (existing == null)
                return false;

            await _repository.DeleteAsync(paymentId);
            return true;
        }
    }
}
