using FluentValidation;
using PaymentService.Application.DTOs;

public class UpdatePaymentValidator : AbstractValidator<PaymentCreateDto>
{
    public UpdatePaymentValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("OrderId is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");


       
    }
}
