using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Projects;

namespace MyOssHours.Backend.Application.Projects;

public static class GetProject
{
    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IProjectsRepository _repository;
        private IUserProvider _userProvider;

        public Handler(IProjectsRepository repository, IUserProvider userProvider)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetProject(request.Uuid, _userProvider.GetCurrentUser().Uuid);

            return new Response
            {
                Project = project
            };
        }
    }

    public class Query : IRequest<Response>
    {
        public Guid Uuid { get; set; }
    }

    public class Response
    {
        public required Project Project { get; set; }
    }
}