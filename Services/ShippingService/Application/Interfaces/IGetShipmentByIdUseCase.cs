using ShippingService.Domain.Entities;

namespace ShippingService.Application.Interfaces
{
    public interface IGetShipmentByIdUseCase
    {
        Task<Shipment?> ExecuteAsync(Guid id);

    }
}
