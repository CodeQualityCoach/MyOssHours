using System.Net.Http.Json;

namespace MyOssHours.Backend.Specs.Tests.Api;

internal class ClientFactory
{
    private static readonly Settings Settings = Settings.Load();

    internal class User
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }

    internal static readonly Dictionary<string, User> Users = new()
    {
        { "alice", new User {  Email = "alice", Password = "password"} },
        { "bob", new User {  Email = "Bob", Password = "password"} },
        { "charlie", new User {  Email = "charlie", Password = "NotAValidPassword"} },
        { "dave", new User {  Email = "dave", Password = "NotAValidUsername"} },
    };

    internal static HttpClient CreateHttpClient()
    {
        var handler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        var client = new HttpClient(handler);
        return client;
    }

    internal static async Task<HttpResponseMessage> Login(HttpClient client, string userid)
    {
        var user = Users[userid.ToLower()];
        return await client.PostAsync(
            $"{Settings.Endpoint}v1/CookieLogin/Login",
            JsonContent.Create(new { user.Email, user.Password }));
    }
}