using PaymentService.Application.Interfaces;
using PaymentService.Domain.Entities;

namespace PaymentService.Application.UseCases
{
    public class GetPaymentByIdUseCase : IGetPaymentByIdUseCase
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentByIdUseCase(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment?> ExecuteAsync(Guid id)
        {
            return await _paymentRepository.GetByIdAsync(id);
        }
    }
}
