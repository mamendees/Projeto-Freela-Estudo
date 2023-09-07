
using Freelancer.Core.Dto;

namespace Freelancer.Core.Services;
public interface IPaymentService
{
    Task<bool> ProcessPayment(PaymentInfoDto paymentInfoDto);
}
