using FluentValidation;
using ShippingService.Application.DTOs;

namespace ShippingService.Application.Validators
{
    public class CreateShipmentDtoValidator : AbstractValidator<CreateShipmentDto>
    {
        public CreateShipmentDtoValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.RecipientName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Address).NotEmpty().MaximumLength(250);
        }
    }
}
