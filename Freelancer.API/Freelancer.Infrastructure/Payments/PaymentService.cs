using Freelancer.Core.Dto;
using Freelancer.Core.Services;
using System.Text;
using System.Text.Json;

namespace Freelancer.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "Payments";
        public PaymentService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        public void ProcessPayment(PaymentInfoDto paymentInfoDto)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDto);
            var paymentInfoJsonBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            _messageBusService.Publish(QUEUE_NAME, paymentInfoJsonBytes);
        }

        //public async Task<bool> ProcessPaymentHttp(PaymentInfoDto paymentInfoDto)
        //{
        //    var url = $"{_paymentsBaseUrl}/api/payments";
        //    var paymentInfoJson = (JsonSerializer.Serialize(paymentInfoDto));
        //    var content = new StringContent(paymentInfoJson, Encoding.UTF8, MediaTypeNames.Application.Json);

        //    var httpClient = _httpClientFactory.CreateClient();
        //    var response = await httpClient.PostAsync(url, content);

        //    return response.IsSuccessStatusCode;
        //}
    }
}
