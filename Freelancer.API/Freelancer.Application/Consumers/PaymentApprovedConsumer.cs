

using Freelancer.Core.IntegrationEvents;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Freelancer.Core.Repositories;

namespace Freelancer.Application.Consumers;
public class PaymentApprovedConsumer : BackgroundService
{
    private const string PAYMENT_APPROVED_QUEUE = "PaymentsApproved";
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;

    public PaymentApprovedConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: PAYMENT_APPROVED_QUEUE, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, aventArgs) =>
        {
            var byteArray = aventArgs.Body.ToArray();
            var paymentApprovedJson = Encoding.UTF8.GetString(byteArray);

            var paymentApprovedIntegrationEvent = JsonSerializer.Deserialize<PaymentApprovedIntegrationEvent>(paymentApprovedJson) ?? throw new Exception();

            await FinishProject(paymentApprovedIntegrationEvent.IdProject);

            _channel.BasicAck(aventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(PAYMENT_APPROVED_QUEUE, false, consumer);

        return Task.CompletedTask;
    }

    private async Task FinishProject(int projectId)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var projectRepository = scope.ServiceProvider.GetRequiredService<IProjectRepository>();

            var project = await projectRepository.GetByIdAsync(projectId);
            if (project == null) return;
            if (!project.SetAndReturnIfCanFinish()) return;

            await projectRepository.SaveChangesAsync();
        }
    }
}
