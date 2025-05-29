using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;

namespace ProductService.Application.UseCases
{
    public class GetProductByIdUseCase : IGetProductByIdUseCase
    {
        private readonly IProductRepository _repository;

        public GetProductByIdUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto?> ExecuteAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock,
                Category = product.Category,
                IsActive = product.IsActive
            };
        }
    }
}
