using ShippingService.Application.DTOs;
using ShippingService.Application.Interfaces;
using ShippingService.Domain.Entities;
using ShippingService.Domain.Enums;

namespace ShippingService.Application.UseCases
{
    public class CreateShipmentUseCase : ICreateShipmentUseCase
    {
        private readonly IShipmentRepository _repository;

        public CreateShipmentUseCase(IShipmentRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(CreateShipmentDto dto)
        {
            var shipment = new Shipment
            {
                Id = Guid.NewGuid(),
                OrderId = dto.OrderId,
                Address = dto.Address,
                ShippedDate = DateTime.UtcNow,
                Status = ShipmentStatus.Pending
                
            };

            await _repository.AddAsync(shipment);
        }
    }

}
