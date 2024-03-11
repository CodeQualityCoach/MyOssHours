using System.Security.Claims;
using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Application.Users;
using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Presentation.Services;

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMediator _mediator;

    public UserProvider(IHttpContextAccessor httpContextAccessor, IMediator mediator)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public User GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext is null) throw new InvalidOperationException("No HTTP context available");

        var email = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
        var name = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
        var sid = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;

        var response = _mediator.Send(new EnsureUser.Command()
        {
            Email = email!,
            Nickname = name!,
            Sid = sid!
        });

        return response.Result.User;
    }
}