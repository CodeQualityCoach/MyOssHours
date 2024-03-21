using System.Net.Http.Json;
using FluentAssertions;
using MyOssHours.Backend.Presentation.Contracts.Models;
using MyOssHours.Backend.Specs.Tests.Api;
using MyOssHours.Backend.Specs.Tests.Shared;
using TechTalk.SpecFlow;

namespace MyOssHours.Backend.Specs.Tests;

[Binding]
public class ProjectStepDefinitions(ScenarioContext context)
{
    private static readonly Settings Settings = Settings.Load();
    private readonly ScenarioContext _context = context ?? throw new ArgumentNullException(nameof(context));

    [When(@"the user creates a new project with the name '([^']*)'")]
    public async Task WhenTheUserCreatesANewProjectWithTheName(string projectName)
    {
        var client = _context.Get<HttpClient>("HttpClient");

        var result = await client.PostAsync(
            $"{Settings.Endpoint}v1/Project",
            JsonContent.Create(new { Name = projectName + "_" + Guid.NewGuid() }));
        _context["StatusCode"] = result.StatusCode;
        _context["Result"] = await result.Content.ReadAsStringAsync();
        _context["ReturnedProject"] = await result.Content.ReadFromJsonAsync<ProjectModel>() ?? throw new ArgumentException("Result cannot be converted as project");
    }

    [Then(@"the project with the name '([^']*)' is created")]
    public void ThenTheProjectWithTheNameIsCreated(string projectName)
    {
        var project = _context.Get<ProjectModel>("ReturnedProject");
        project.Name.Should().StartWith(projectName + "_");
    }

    [When(@"the user reads the existing projects")]
    [Then(@"the user reads the existing projects")]
    public async Task WhenTheUserReadsExistingProjects()
    {
        var client = _context.Get<HttpClient>("HttpClient");
        var result = await client.GetAsync($"{Settings.Endpoint}v1/Project?Size=500");

        _context["StatusCode"] = result.StatusCode;
        _context["Result"] = await result.Content.ReadAsStringAsync();
        _context["ReturnedProjects"] = await result.Content.ReadFromJsonAsync<IEnumerable<ProjectModel>>() ?? throw new ArgumentException("Result cannot be converted as project list");
    }

    [Then(@"the result contains a project with the name '([^']*)'")]
    public void ThenTheResultContainsAProjectWithTheName(string projectName)
    {
        var initProject = _context.Get<IEnumerable<BackgroundStepDefinitions.ProjectTestDto>>("InitProjects")
            .First(x=>x.Name.StartsWith(projectName + "_"));

        var projects = _context.Get<IEnumerable<ProjectModel>>("ReturnedProjects").Where(p => p.Name.StartsWith(projectName));
        var project = projects.FirstOrDefault(p => p.Name == initProject.Name);
        project.Should().NotBeNull();
    }

    [Then(@"the result does not contain a project with the name '([^']*)'")]
    public void ThenTheResultDoesNotContainAProjectWithTheName(string projectName)
    {
        var initProject = _context.Get<IEnumerable<BackgroundStepDefinitions.ProjectTestDto>>("InitProjects")
            .First(x => x.Name.StartsWith(projectName + "_"));

        var projects = _context.Get<IEnumerable<ProjectModel>>("ReturnedProjects").Where(p=>p.Name.StartsWith(projectName));
        var project = projects.FirstOrDefault(p => p.Name == initProject.Name);
        project.Should().BeNull();
    }

    [When(@"the user deletes the project '([^']*)'")]
    public async Task WhenTheUserDeletesTheProject(string projectName)
    {
        var initProject = _context.Get<IEnumerable<BackgroundStepDefinitions.ProjectTestDto>>("InitProjects")
            .First(x => x.Name.StartsWith(projectName + "_"));

        var client = _context.Get<HttpClient>("HttpClient");
        var result = await client.DeleteAsync($"{Settings.Endpoint}v1/Project/{initProject.Uuid}");

        _context["StatusCode"] = result.StatusCode;
        _context["Result"] = await result.Content.ReadAsStringAsync();
    }

}