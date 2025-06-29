using ShippingService.Application.Interfaces;
using ShippingService.Domain.Entities;

namespace ShippingService.Application.UseCases;

public class GetAllShipments
{
    private readonly IShipmentRepository _repository;

    public GetAllShipments(IShipmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Shipment>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}
