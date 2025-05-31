using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using System.Text.Json;

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
            var response = await _httpClient.GetAsync($"http://productservice/api/Products/{productId}");
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($" ProductService yanıt kodu: {response.StatusCode}");
            Console.WriteLine($" Yanıt içeriği: {content}");

            if (!response.IsSuccessStatusCode)
                return null;

            return JsonSerializer.Deserialize<ProductDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        }
    }
}
