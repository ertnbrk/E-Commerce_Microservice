using ProductService.Application.DTOs;

namespace ProductService.Application.Interfaces
{
    public interface ICreateProductUseCase
    {
        Task<Guid> ExecuteAsync(ProductDto dto);


    }
}
