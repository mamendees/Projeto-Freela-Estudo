

namespace Freelancer.Core.Dto;
public class PaymentInfoDto
{
    public PaymentInfoDto(int idProject, string creditCardNumber, string cvv, string expiresAt, string fullName, decimal amount)
    {
        IdProject = idProject;
        CreditCardNumber = creditCardNumber;
        Cvv = cvv;
        ExpiresAt = expiresAt;
        FullName = fullName;
        Amount = amount;
    }

    public int IdProject { get; set; }
    public string CreditCardNumber { get; set; } = string.Empty;
    public string Cvv { get; set; } = string.Empty;
    public string ExpiresAt { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
