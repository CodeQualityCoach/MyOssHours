using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Application.Users;

public class EnsureUser
{
    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IUserRepository _repository;

        public Handler(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Response> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await _repository.EnsureUser(command.Sid, command.Nickname, command.Email);

            return new Response
            {
                User = user
            };
        }
    }

    public class Command : IRequest<Response>
    {
        public required string Sid { get; set; }
        public required string Email { get; set; }
        public required string Nickname { get; set; }
    }

    public class Response
    {
        public required User User { get; set; }
    }
}