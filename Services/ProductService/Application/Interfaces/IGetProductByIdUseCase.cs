using ProductService.Application.DTOs;

namespace ProductService.Application.Interfaces
{
    public interface IGetProductByIdUseCase
    {
        Task<ProductDto?> ExecuteAsync(Guid id);

    }
}
