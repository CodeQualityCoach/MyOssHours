using System.Net.Http.Json;
using FluentAssertions;
using MyOssHours.Backend.Presentation.Controllers;
using MyOssHours.Backend.Presentation.Models;
using TechTalk.SpecFlow;

namespace MyOssHours.Backend.Specs.Tests;

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
            var result = await client.PostAsync($"https://localhost:10443/api/v1/Project", JsonContent.Create(new ProjectController.CreateProjectCommand()
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