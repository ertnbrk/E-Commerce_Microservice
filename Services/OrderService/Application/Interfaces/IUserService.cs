namespace OrderService.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> UserExistsAsync(Guid userId);

    }
}
