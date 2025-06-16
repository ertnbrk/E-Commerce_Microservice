
using PaymentService.Application.Interfaces;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;

namespace OrderService.Application.UseCases
{
    public class GetPaymentByStatusUseCase : IGetPaymentStatusUseCase
    {
        private readonly IPaymentRepository _repository;

        public GetPaymentByStatusUseCase(IPaymentRepository repository)
        {
            _repository = repository;
        }

       
       

        public async Task<List<Payment>> ExecuteAsync(PaymentStatusEnum status)
        {
            return (List<Payment>)await _repository.GetByStatusAsync(status);
        }
    }
}
