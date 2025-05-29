using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;

namespace OrderService.Infrastructure.Services
{
    public class ProductHttpService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductHttpService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<ProductDto?> GetProductByIdAsync(Guid productId)
        {
            var response = await _httpClient.GetAsync($"http://productservice/api/products/{productId}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }
    }
}
