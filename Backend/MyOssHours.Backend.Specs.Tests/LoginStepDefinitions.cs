using FluentAssertions;
using MyOssHours.Backend.Specs.Tests.Api;
using MyOssHours.Backend.Specs.Tests.Shared;
using TechTalk.SpecFlow;

namespace MyOssHours.Backend.Specs.Tests;

[Binding]
public class LoginStepDefinitions(ScenarioContext context)
{
    private static readonly Settings Settings = Settings.Load();
    private readonly ScenarioContext _context = context ?? throw new ArgumentNullException(nameof(context));

    [Given(@"the user '([^']*)' is logged in")]
    [When(@"the user '([^']*)' is logged in")]
    // NOT WORKING [When( @"the user {string} is logged in")]
    public async Task GivenTheUserWithIdIsLoggedIn(string id)
    {
        var client = ClientFactory.CreateHttpClient();
        var result = await ClientFactory.Login(client, id);

        _context["StatusCode"] = result.StatusCode;
        _context["HttpClient"] = client;
    }

    [When(@"the user information is requested")]
    public async Task WhenTheUserInformationIsRequested()
    {
        var client = _context.ContainsKey("HttpClient")
            ? _context.Get<HttpClient>("HttpClient")
            : ClientFactory.CreateHttpClient();

        var result = await client.GetAsync($"{Settings.Endpoint}v1/CookieLogin/GetCurrentUser");

        _context["StatusCode"] = result.StatusCode;
        _context["Result"] = await result.Content.ReadAsStringAsync();
    }

    [Then(@"the user '([^']*)' has a claim containing his email address")]
    // NOT WORKING [Then(@"the user has a claim with an email address containing {string}")]
    public void ThenTheUserHasAClaimWithAnEmailAddressContaining(string id)
    {
        var result = _context.Get<string>("Result");
        result.Should().Contain(ClientFactory.Users[id.ToLower()].Email);
    }
}