namespace ProductService.Application.Interfaces
{
    public interface IDeleteProductUseCase
    {
        Task<bool> ExecuteAsync(Guid id);

    }
}
