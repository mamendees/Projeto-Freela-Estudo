using Freelancer.Core.Dto;
using Freelancer.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Freelancer.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _msalHttpClientFactory;
        private readonly string _paymentsBaseUrl;
        public PaymentService(IHttpClientFactory msalHttpClientFactory, IConfiguration configuration)
        {
            _msalHttpClientFactory = msalHttpClientFactory;
            _paymentsBaseUrl = configuration.GetSection("Services:Payments").Value ?? throw new Exception("Exception");
        }

        public async Task<bool> ProcessPayment(PaymentInfoDto paymentInfoDto)
        {
            var url = $"{_paymentsBaseUrl}/api/payments";
            var paymentInfoJson = (JsonSerializer.Serialize(paymentInfoDto));
            var content = new StringContent(paymentInfoJson, Encoding.UTF8, MediaTypeNames.Application.Json);;
            
            var httpClient = _msalHttpClientFactory.CreateClient();
            var response = await httpClient.PostAsync(url, content);

            return response.IsSuccessStatusCode;
        }
    }
}
