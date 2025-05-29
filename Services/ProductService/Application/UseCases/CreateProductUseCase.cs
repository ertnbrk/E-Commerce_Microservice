using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.Application.UseCases
{
    public class CreateProductUseCase : ICreateProductUseCase
    {
        private readonly IProductRepository _repository;

        public CreateProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> ExecuteAsync(ProductDto dto)
        {
            var product = new Product
            {
                ProductId = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                UnitPrice = dto.UnitPrice,
                Stock = dto.Stock,
                Category = dto.Category,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            return product.ProductId;
        }
    }
}
