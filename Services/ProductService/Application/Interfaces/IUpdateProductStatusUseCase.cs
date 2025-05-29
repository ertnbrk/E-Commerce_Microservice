using ProductService.Application.DTOs;

namespace ProductService.Application.Interfaces
{
    public interface IUpdateProductStatusUseCase
    {
        Task<bool> ExecuteAsync(Guid id, UpdateProductStatusDto dto);

    }
}
