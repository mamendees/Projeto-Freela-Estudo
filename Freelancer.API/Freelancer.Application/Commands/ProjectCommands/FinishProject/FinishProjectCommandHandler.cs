using Freelancer.Core.Dto;
using Freelancer.Core.Repositories;
using Freelancer.Core.Services;
using MediatR;

namespace Freelancer.Application.Commands.ProjectCommands.FinishProject;
public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, Unit>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IPaymentService _paymentService;
    public FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentService paymentService)
    {
        _projectRepository = projectRepository;
        _paymentService = paymentService;
    }

    public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);
        if (project is null) return Unit.Value;

        var paymentInfoDto = new PaymentInfoDto(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName, project.TotalCost);
        _paymentService.ProcessPayment(paymentInfoDto);

        project.SetPaymentPending();

        await _projectRepository.SaveChangesAsync();
        return Unit.Value;
    }
}
