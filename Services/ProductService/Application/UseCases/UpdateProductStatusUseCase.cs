using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;

namespace ProductService.Application.UseCases
{
    public class UpdateProductStatusUseCase : IUpdateProductStatusUseCase
    {
        private readonly IProductRepository _repository;

        public UpdateProductStatusUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(Guid id, UpdateProductStatusDto dto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return false;

            if (dto.IsActive.HasValue)
                product.IsActive = dto.IsActive.Value;

            if (dto.UnitPrice.HasValue)
                product.UnitPrice = dto.UnitPrice.Value;

            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
