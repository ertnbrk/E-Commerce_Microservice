using FluentValidation;
using OrderService.Application.DTOs;

namespace OrderService.Application.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
