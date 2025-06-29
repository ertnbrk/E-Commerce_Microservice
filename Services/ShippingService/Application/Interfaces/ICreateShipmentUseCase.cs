using ShippingService.Application.DTOs;

namespace ShippingService.Application.Interfaces
{
    public interface ICreateShipmentUseCase
    {
        Task ExecuteAsync(CreateShipmentDto dto);

    }
}
