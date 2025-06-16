namespace PaymentService.Application.Interfaces
{
    public interface ICancelPaymentUseCase
    {
        Task<bool> ExecuteAsync(Guid paymentId);

    }
}
