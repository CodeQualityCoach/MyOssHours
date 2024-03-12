using System.Security.Claims;
using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Application.Users;
using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Presentation.Services;

public class UserProvider
    (IHttpContextAccessor httpContextAccessor, IMediator mediator)
    : IUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public User GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext is null) throw new InvalidOperationException("No HTTP context available");

        var email = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
        var name = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;

        var response = _mediator.Send(new EnsureUser.Command()
        {
            Email = email!,
            Nickname = name!,
        });

        return response.Result.User;
    }
}