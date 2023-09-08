
using Freelancer.Payments.API.Models;
using Freelancer.Payments.API.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Freelancer.Payments.API.Consumers;
public class ProcessPaymentConsumer : BackgroundService
{
    private const string QUEUE_NAME = "Payments";
    private const string PAYMENT_APPROVED_QUEUE = "PaymentsApproved";

    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;

    public ProcessPaymentConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: QUEUE_NAME, durable: false, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueDeclare(queue: PAYMENT_APPROVED_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (sender, aventArgs) =>
        {
            var byteArray = aventArgs.Body.ToArray();
            var paymentInfoJson = Encoding.UTF8.GetString(byteArray);

            var paymentInfo = JsonSerializer.Deserialize<PaymentInfoInputModel>(paymentInfoJson) ?? throw new Exception();

            ProcessPayment(paymentInfo);

            var paymentApproved = new PaymentApprovedIntegrationEvent(paymentInfo.IdProject);
            var paymentApprovedJson = JsonSerializer.Serialize(paymentApproved) ?? throw new Exception();
            var paymentApprovedBytes = Encoding.UTF8.GetBytes(paymentApprovedJson);

            _channel.BasicPublish(exchange: "", routingKey: PAYMENT_APPROVED_QUEUE, basicProperties: null, body: paymentApprovedBytes);
            _channel.BasicAck(aventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(QUEUE_NAME, false, consumer);

        return Task.CompletedTask;
    }

    private void ProcessPayment(PaymentInfoInputModel paymentInfo)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            paymentService.Process(paymentInfo);
        }
    }
}
