using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Projects;

namespace MyOssHours.Backend.Application.Projects;

public static class GetProjects
{
    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IProjectsRepository _repository;
        private readonly IUserProvider _userProvider;

        public Handler(IProjectsRepository repository, IUserProvider userProvider)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetProjects(_userProvider.GetCurrentUser().Uuid, request.Offset, request.Size);

            return new Response
            {
                Projects = projects
            };
        }
    }

    public class Query : IRequest<Response>
    {
        public int Offset { get; set; }
        public int Size { get; set; }
        public string? NameLike { get; set; }
    }

    public class Response
    {
        public required IEnumerable<Project> Projects { get; set; }
    }
}