using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Presentation.Services;

namespace MyOssHours.Backend.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAutoMapper(typeof(AssemblyReference))
            .AddControllers()
            .AddApplicationPart(typeof(AssemblyReference).Assembly);

        services.AddScoped<IUserProvider, UserProvider>();

        services.AddHttpContextAccessor();

        return services;
    }

    public static IApplicationBuilder UsePresentation(this IApplicationBuilder app)
    {
        //// Todo: Move this to a middleware
        //app.Use(async (context, next) =>
        //{
        //    var mediator = context.RequestServices.GetService<IMediator>() ?? throw new ArgumentException();
        //    var httpContext = context.RequestServices.GetService<IHttpContextAccessor>();
        //    var userClaim = httpContext?.HttpContext?.User;
        //    if (userClaim is { Identity: not null } && userClaim.Claims.Count() > 5)
        //    {
        //        var userSid = userClaim.FindFirst("sid")?.Value ?? throw new ArgumentException();

        //        var response = await mediator.Send(new EnsureUser.Command
        //        {
        //            Sid = userSid,
        //            Email = userClaim.FindFirst("email")?.Value ?? string.Empty,
        //            Nickname = userClaim.FindFirst("nickname")?.Value ?? string.Empty
        //        });

        //        context.Items.Add("User", response.User);
        //    }

        //    // Call the next delegate/middleware in the pipeline.
        //    await next(context);
        //});

        ((IEndpointRouteBuilder)app)
            .MapControllers()
            .RequireAuthorization();

        return app;
    }
}