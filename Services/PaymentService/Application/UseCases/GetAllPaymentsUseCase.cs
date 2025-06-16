using PaymentService.Application.Interfaces;
using PaymentService.Domain.Entities;

namespace PaymentService.Application.UseCases
{
    public class GetAllPaymentsUseCase : IGetAllPaymentsUseCase
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetAllPaymentsUseCase(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<List<Payment>> ExecuteAsync()
        {
            var payments = (await _paymentRepository.GetAllAsync()).ToList();
            return payments;
        }
    }
}
