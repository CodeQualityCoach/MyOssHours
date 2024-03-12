using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Projects;

namespace MyOssHours.Backend.Application.WorkItems;

public static class CreateWorkItem
{
    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IWorkItemsRepository _repository;

        public Handler(IWorkItemsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var workItem = await _repository.CreateWorkItem(request.Project, request.Name, request.Description);

            return new Response
            {
                WorkItem = workItem
            };
        }
    }

    public class Command : IRequest<Response>
    {
        public required ProjectId Project { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }

    public class Response
    {
        public required WorkItem WorkItem { get; set; }
    }
}