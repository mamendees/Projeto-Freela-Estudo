using Freelancer.Payments.API.Models;
using Freelancer.Payments.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Freelancer.Payments.API.Controllers;
[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PaymentInfoInputModel paymentInfoInputModel)
    {
        var result = await _paymentService.Process(paymentInfoInputModel);

        if (!result) return BadRequest();
        return Ok(result);
    }
}
