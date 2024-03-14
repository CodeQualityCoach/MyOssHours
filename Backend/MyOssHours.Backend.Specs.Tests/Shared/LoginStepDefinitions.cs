using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace MyOssHours.Backend.Specs.Tests.Shared;

[Binding]
public class LoginStepDefinitions(ScenarioContext context)
{
    private readonly ScenarioContext _context = context ?? throw new ArgumentNullException(nameof(context));

    private class User
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }

    private readonly Dictionary<string, User> _users = new()
    {
        { "Alice", new User {  Email = "alice", Password = "password"} },
        { "Bob", new User {  Email = "Bob", Password = "password"} },
        { "Charlie", new User {  Email = "charlie", Password = "NotAValidPassword"} },
    };

    [Given(@"the user (.*) is logged in")]
    public async Task GivenTheUserWithIdIsLoggedIn(string id)
    {
        var handler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        var client = new HttpClient(handler);
        var user = _users[id];
        var result = await client.PostAsync($"https://localhost:10443/api/v1/CookieLogin/Login/", JsonContent.Create(user));

        _context["StatusCode"] = result.StatusCode;
        _context["HttpClient"] = client;
    }

    [When(@"the user information is requested")]
    public async Task WhenTheUserInformationIsRequested()
    {
        var client = _context.Get<HttpClient>("HttpClient");

        var result = await client.GetAsync($"https://localhost:10443/api/v1/CookieLogin/GetCurrentUser");
        result.EnsureSuccessStatusCode();
        _context["Result"] = await result.Content.ReadAsStringAsync();
    }

    [Then(@"the user has a claim with an email address containing (.*)")]
    public void ThenTheUserHasAClaimWithAnEmailAddressContaining(string id)
    {
        var result = _context.Get<string>("Result");
    }

    [Then(@"a (.*) is returned")]
    public void ThenAUnauthorizedIsReturned(int returnCode)
    {
        var statusCode = _context.Get<HttpStatusCode>("StatusCode");
        statusCode.Should().Be((HttpStatusCode)returnCode);
    }

}