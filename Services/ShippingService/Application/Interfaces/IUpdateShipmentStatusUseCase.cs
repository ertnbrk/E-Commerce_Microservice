namespace ShippingService.Application.Interfaces
{
    public interface IUpdateShipmentStatusUseCase
    {
        Task ExecuteAsync(Guid id, string newStatus);

    }
}
