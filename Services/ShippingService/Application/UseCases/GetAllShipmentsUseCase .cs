using ShippingService.Application.Interfaces;
using ShippingService.Domain.Entities;

namespace ShippingService.Application.UseCases
{
    public class GetAllShipmentsUseCase : IGetAllShipmentsUseCase
    {
        private readonly IShipmentRepository _shipmentRepository;

        public GetAllShipmentsUseCase(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<List<Shipment>> ExecuteAsync()
        {
            return await _shipmentRepository.GetAllAsync();
        }
    }
}
