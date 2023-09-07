
namespace Freelancer.Payments.API.Models;
public class PaymentInfoInputModel
{
    public int IdProject { get; set; }
    public string CreditCardNumber { get; set; } = string.Empty;
    public string Cvv { get; set; } = string.Empty;
    public string ExpiresAt { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
