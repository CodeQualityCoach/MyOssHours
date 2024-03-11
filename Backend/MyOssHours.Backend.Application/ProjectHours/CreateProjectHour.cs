using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Application.Projects;
using MyOssHours.Backend.Domain.Projects;
using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Application.ProjectHours;

public static class CreateProjectHour
{
    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IProjectHoursRepository _repository;
        private readonly IMediator _mediator;

        public Handler(IProjectHoursRepository repository, IMediator mediator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var projectResponse = await _mediator.Send(new GetProject.Query() { Uuid = request.Project }, cancellationToken);
            var tmpHour = projectResponse.Project.CreateProjectHour(request.WorkItem, request.User, request.Date, request.Duration, request.Description);
            var projectHour = await _repository.CreateProjectHour(tmpHour);

            return new Response
            {
                ProjectHour = projectHour
            };
        }
    }

    public class Command : IRequest<Response>
    {
        public required ProjectId Project { get; set; }
        public required WorkItemId WorkItem { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public required UserId User { get; set; }
        public string? Description { get; set; }
    }

    public class Response
    {
        public required ProjectHour ProjectHour { get; set; }
    }
}