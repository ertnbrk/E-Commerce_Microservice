using FluentValidation;
using PaymentService.Application.DTOs;

public class CreatePaymentDtoValidator : AbstractValidator<PaymentCreateDto>
{
    public CreatePaymentDtoValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("OrderId is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");


       
    }
}
