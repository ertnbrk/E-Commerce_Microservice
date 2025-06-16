using PaymentService.Application.DTOs;
using PaymentService.Application.Interfaces;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;

namespace PaymentService.Application.UseCases
{
    public class CreatePaymentUseCase : ICreatePaymentUseCase
    {
        private readonly IPaymentRepository _paymentRepository;

        public CreatePaymentUseCase(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Guid> ExecuteAsync(PaymentCreateDto dto)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = dto.OrderId,
                UserId = dto.UserId,
                Amount = dto.Amount,
                PaymentDate = DateTime.UtcNow,
                Method = dto.Method,
                Notes = dto.Notes,
                Status = PaymentStatusEnum.Pending,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _paymentRepository.AddAsync(payment);
            return payment.Id;
        }
    }
}
