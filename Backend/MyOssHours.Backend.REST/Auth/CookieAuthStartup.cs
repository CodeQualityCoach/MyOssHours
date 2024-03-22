using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyOssHours.Backend.REST.Auth;

public static class CookieAuthStartup
{
    public static IServiceCollection AddCookieAuth(IServiceCollection services)
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.ConsentCookie.IsEssential = true;
            options.CheckConsentNeeded = context => false;

        });

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 403;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };                                      
            });

        services.AddMvc().AddControllersAsServices();
        services.AddScoped<CookieLoginController>();

        return services;
    }

    // UseAuthorization
    public static IApplicationBuilder UseCookieAuth(IApplicationBuilder app)
    {
        app.UseAuthentication().UseCookiePolicy();
        app.UseAuthorization();

        return app;
    }
}