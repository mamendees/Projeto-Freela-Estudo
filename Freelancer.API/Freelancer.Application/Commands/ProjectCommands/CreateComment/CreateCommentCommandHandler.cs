﻿using Freelancer.Core.Entities;
using Freelancer.Core.Repositories;
using MediatR;

namespace Freelancer.Application.Commands.ProjectCommands.CreateComment;
public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
{
    private readonly IProjectRepository _projectRepository;
    public CreateCommentCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);
        await _projectRepository.CreateCommentAsync(comment);
        return Unit.Value;
    }
}
