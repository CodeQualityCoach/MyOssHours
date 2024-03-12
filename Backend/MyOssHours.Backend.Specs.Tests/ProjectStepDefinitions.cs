using System.Net.Http.Json;
using FluentAssertions;
using MyOssHours.Backend.Presentation.Models;
using MyOssHours.Backend.Presentation.Requests;
using TechTalk.SpecFlow;

namespace MyOssHours.Backend.Specs.Tests;

[Binding]
public class LoginStepDefinitions
{
    private readonly ScenarioContext _context;

    private class User
    {
        public required string Name { get; init; }
        public required string Email { get; init; }
    }

    public LoginStepDefinitions(ScenarioContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    private readonly Dictionary<string, User> _users = new()
    {
        { "alice", new User { Name = "alice", Email = "alice@localhost"} },
        { "bob", new User { Name = "bob", Email = "bob@localhost"} },
    };

    [Given(@"the user (.*) is logged in")]
    public async Task GivenTheUserWithIdIsLoggedIn(string id)
    {
        var client = new HttpClient();
        var user = _users[id];
        var result = await client.PostAsync($"https://localhost:10443/api/v1/CookieLogin/Login/", new StringContent("{\r\n  \"email\": \"Thomas\",\r\n  \"password\": \"password123\"\r\n}"));

        _context["HttpClient"] = client;
    }
}

[Binding]
public class ProjectStepDefinitions
{
    private readonly ScenarioContext _context;

    public ProjectStepDefinitions(ScenarioContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [Given(@"the following projects exist for user alice:")]
    public async Task GivenTheFollowingProjectsExistForUserAlice(Table table)
    {
        var client = _context.Get<HttpClient>("HttpClient");
        foreach (var tableRow in table.Rows)
        {
            var result = await client.PostAsync($"https://localhost:6003/api/v1/Project", JsonContent.Create(new CreateProjectCommand()
            {
                Name = tableRow["name"],
                Description = tableRow["description"],
            }));
            result.EnsureSuccessStatusCode();
        }
    }

    [When(@"the user creates a new project with the name '([^']*)'")]
    public async Task WhenTheUserCreatesANewProjectWithTheName(string projectname)
    {
        throw new PendingStepException();

    }

    [Then(@"the project with the name '([^']*)' is created")]
    public void ThenTheProjectWithTheNameIsCreated(string p0)
    {
        throw new PendingStepException();
    }

    [When(@"the user (.*) reads the existing projects")]
    public async Task WhenTheUserReadsExistingProjects(string user)
    {
        var client = _context.Get<HttpClient>("HttpClient");
        var result = await client.GetAsync($"https://localhost:6003/api/v1/Project");
        result.EnsureSuccessStatusCode();

        var projects = await result.Content.ReadFromJsonAsync<IEnumerable<ProjectModel>>();
        _context["Projects"] = projects;
    }

    [Then(@"the result contains a project with the name '([^']*)'")]
    public void ThenTheResultContainsAProjectWithTheName(string p0)
    {
        var projects = _context.Get<IEnumerable<ProjectModel>>("Projects");
        projects.Should().Contain(p => p.Name == p0);
    }

    [Then(@"the result does not contain a project with the name '([^']*)'")]
    public void ThenTheResultDoesNotContainAProjectWithTheName(string p0)
    {
        throw new PendingStepException();
    }

}