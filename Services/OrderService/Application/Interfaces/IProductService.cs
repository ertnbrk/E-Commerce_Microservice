using OrderService.Application.DTOs;

namespace OrderService.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto?> GetProductByIdAsync(Guid productId);

    }
}
