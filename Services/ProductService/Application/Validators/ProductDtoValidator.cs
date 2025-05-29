using FluentValidation;
using ProductService.Application.DTOs;

namespace ProductService.Application.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100);

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Invalid product category.");
        }
    }
}
