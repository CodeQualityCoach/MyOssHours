namespace MyOssHours.Backend.REST.Auth;

public static class AuthExtensions
{
    public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
    {
        // add a default
        var setting = builder.Configuration.GetSection("Auth").Get<AuthSettings>();

        if (setting?.Validator?.Type == "HtAccessUserVerification")
        {
            builder.Services.AddScoped<IUserValidator, HtAccessUserVerification>();
        }

        if (setting?.Type == "Cookie")
            CookieAuthStartup.AddCookieAuth(builder.Services);

        // todo learn me
        //builder.Services.AddAuthorization(options =>
        //{
        //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        //    options.AddPolicy("User", policy => policy.RequireRole("User"));
        //});

        return builder;
    }
}