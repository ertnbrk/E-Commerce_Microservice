using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;

namespace ProductService.Application.UseCases
{
    public class GetAllProductsUseCase : IGetAllProductsUseCase
    {
        private readonly IProductRepository _repository;

        public GetAllProductsUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductDto>> ExecuteAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                UnitPrice = p.UnitPrice,
                Stock = p.Stock,
                Category = p.Category,
                IsActive = p.IsActive
            }).ToList();
        }
    }
}
