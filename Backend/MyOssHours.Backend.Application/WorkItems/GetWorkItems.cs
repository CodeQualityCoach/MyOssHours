using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Projects;

namespace MyOssHours.Backend.Application.WorkItems;

public static class GetWorkItems
{
    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IWorkItemsRepository _repository;
        private readonly IUserProvider _userProvider;

        public Handler(IWorkItemsRepository repository, IUserProvider userProvider)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var workItems = await _repository.GetWorkItems(_userProvider.GetCurrentUser().Uuid, request.Project);

            return new Response
            {
                WorkItems = workItems
            };
        }
    }

    public class Query : IRequest<Response>
    {
        public required ProjectId Project { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }
    }

    public class Response
    {
        public required IEnumerable<WorkItem> WorkItems { get; set; }
    }
}