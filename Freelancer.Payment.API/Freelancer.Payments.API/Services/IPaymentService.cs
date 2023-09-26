
using Freelancer.Payments.API.Models;

namespace Freelancer.Payments.API.Services;
public interface IPaymentService
{
    Task<bool> Process(PaymentInfoInputModel paymentInfoInputModel);
}
