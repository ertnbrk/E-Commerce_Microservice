using ShippingService.Domain.Entities;

namespace ShippingService.Application.Interfaces
{
    public interface IGetAllShipmentsUseCase
    {
        Task<List<Shipment>> ExecuteAsync();

    }
}
