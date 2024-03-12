using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Projects;
using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Application.Projects;

public static class CreateProject
{
    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IProjectsRepository _repository;

        public Handler(IProjectsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var project = await _repository.CreateProject(Project.Create(request.Name, request.Description, request.Owner.Uuid));

            return new Response
            {
                Project = project
            };
        }
    }

    public class Command : IRequest<Response>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required UserId Owner { get; set; }
    }

    public class Response
    {
        public required Project Project { get; set; }
    }
}