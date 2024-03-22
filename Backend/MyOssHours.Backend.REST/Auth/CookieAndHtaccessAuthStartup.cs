using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyOssHours.Backend.REST.Auth;

public static class CookieAndHtaccessAuthStartup
{
    public static IServiceCollection AddCookieAuth(IServiceCollection services)
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.ConsentCookie.IsEssential = true;
            options.CheckConsentNeeded = context => false;

        });

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

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