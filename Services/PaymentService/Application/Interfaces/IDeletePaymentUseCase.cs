namespace PaymentService.Application.Interfaces
{
    public interface IDeletePaymentUseCase
    {
        Task<bool> ExecuteAsync(Guid paymentId);

    }
}
