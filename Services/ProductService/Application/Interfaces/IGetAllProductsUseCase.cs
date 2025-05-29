using ProductService.Application.DTOs;

namespace ProductService.Application.Interfaces
{
    public interface IGetAllProductsUseCase
    {
        Task<List<ProductDto>> ExecuteAsync();

    }
}
