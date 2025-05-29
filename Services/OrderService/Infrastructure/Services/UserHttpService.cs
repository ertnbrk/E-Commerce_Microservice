using OrderService.Application.Interfaces;

namespace OrderService.Infrastructure.Services
{
    public class UserHttpService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> UserExistsAsync(Guid userId)
        {
            var response = await _httpClient.GetAsync($"http://userservice/api/users/{userId}");
            return response.IsSuccessStatusCode;
        }
    }
}
