using PaymentService.Application.Interfaces;

namespace PaymentService.Application.UseCases
{
    public class CancelPaymentUseCase : ICancelPaymentUseCase
    {
        private readonly IPaymentRepository _repository;

        public CancelPaymentUseCase(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(Guid paymentId)
        {
            var payment = await _repository.GetByIdAsync(paymentId);
            if (payment == null)
                return false;

            await _repository.DeleteAsync(paymentId);
            return true;
        }
    }
}
