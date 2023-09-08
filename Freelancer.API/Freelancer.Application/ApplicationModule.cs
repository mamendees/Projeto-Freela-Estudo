using Freelancer.Application.Commands.ProjectCommands.CreateProject;
using Freelancer.Application.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace Freelancer.Application;
public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateProjectCommand))).AddConsumers();

        return services;
    }

    private static IServiceCollection AddConsumers(this IServiceCollection services)
    {
        services.AddHostedService<PaymentApprovedConsumer>();

        return services;
    }
}
