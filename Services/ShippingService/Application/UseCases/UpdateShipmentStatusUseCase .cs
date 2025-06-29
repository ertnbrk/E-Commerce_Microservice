using ShippingService.Application.Interfaces;
using ShippingService.Domain.Enums;

namespace ShippingService.Application.UseCases
{
    public class UpdateShipmentStatusUseCase : IUpdateShipmentStatusUseCase
    {
        private readonly IShipmentRepository _shipmentRepository;

        public UpdateShipmentStatusUseCase(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task ExecuteAsync(Guid id, string newStatus)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(id);
            if (shipment == null)
                throw new Exception("Kargo bulunamadı.");

            if (!Enum.TryParse<ShipmentStatus>(newStatus, true, out var parsedStatus))
                throw new Exception("Geçersiz kargo durumu.");

            shipment.Status = parsedStatus;
            shipment.ModifiedAt = DateTime.UtcNow;

            await _shipmentRepository.UpdateAsync(shipment);
        }
    }
}
