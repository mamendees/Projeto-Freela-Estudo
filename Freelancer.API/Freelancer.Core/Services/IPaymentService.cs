
using Freelancer.Core.Dto;

namespace Freelancer.Core.Services;
public interface IPaymentService
{
    void ProcessPayment(PaymentInfoDto paymentInfoDto);
}
