
using Freelancer.Payments.API.Models;

namespace Freelancer.Payments.API.Services;
public class PaymentService : IPaymentService
{
    public Task<bool> Process(PaymentInfoInputModel paymentInfoInputModel)
    {
        return Task.FromResult(true);
    }
}
