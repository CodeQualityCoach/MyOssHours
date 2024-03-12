using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Projects;

namespace MyOssHours.Backend.Application.WorkItems;

public static class DeleteWorkItem
{
    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IWorkItemsRepository _repository;
        private readonly IUserProvider _userProvider;

        public Handler(IWorkItemsRepository repository, IUserProvider userProvider)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteWorkItem(_userProvider.GetCurrentUser().Uuid, request.WorkItem);

            return new Response { Success = result };
        }
    }

    public class Command : IRequest<Response>
    {
        public required WorkItemId WorkItem { get; set; }
    }

    public class Response
    {
        public required bool Success { get; init; }
        public Exception? Exception { get; init; }
    }
}