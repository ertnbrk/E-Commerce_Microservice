using ShippingService.Application.Interfaces;
using ShippingService.Domain.Entities;

namespace ShippingService.Application.UseCases
{
    public class GetShipmentByIdUseCase : IGetShipmentByIdUseCase
    {
        private readonly IShipmentRepository _shipmentRepository;

        public GetShipmentByIdUseCase(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<Shipment?> ExecuteAsync(Guid id)
        {
            return await _shipmentRepository.GetByIdAsync(id);
        }
    }
}
