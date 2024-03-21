using System.Net.Http.Json;
using MyOssHours.Backend.Presentation.Contracts.Models;
using MyOssHours.Backend.Specs.Tests.Api;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace MyOssHours.Backend.Specs.Tests.Shared;

[Binding]
public class BackgroundStepDefinitions(ScenarioContext context)
{
    private static readonly Settings Settings = Settings.Load();
    private readonly ScenarioContext _context = context ?? throw new ArgumentNullException(nameof(context));

    internal class ProjectTestDto
    {
        public Guid Uuid { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Permissions { get; set; }
        public Dictionary<string, string> PermissionDict { get; set; } = new();
    }

    [Given(@"the following projects exist:")]
    public async Task GivenTheFollowingProjectsExist(Table table)
    {
        var project = table.CreateSet<ProjectTestDto>();
        foreach (var p in project)
        {
            p.PermissionDict = p.Permissions.Split(',').Select(s => s.Split('=')).ToDictionary(s => s[0], s => s[1]);
            p.Name += "_" + Guid.NewGuid();
            var tempClient = ClientFactory.CreateHttpClient();
            var owner = p.PermissionDict.FirstOrDefault(x => x.Value == "owner");
            var loginResult = await ClientFactory.Login(tempClient, owner.Key);
            loginResult.EnsureSuccessStatusCode();

            var projectResult = await tempClient.PostAsync(
                $"{Settings.Endpoint}v1/Project",
                JsonContent.Create(new { p.Name, p.Description }));
            projectResult.EnsureSuccessStatusCode();
            var returnedProject = await projectResult.Content.ReadFromJsonAsync<ProjectModel>() ?? throw new ArgumentException("Result cannot be converted as project");
            p.Uuid = returnedProject.Uuid;

            // todo add permissions
            foreach (var permission in p.PermissionDict)
            {
                if (permission.Key == owner.Key) continue;
            }
        }

        _context["InitProjects"] = project;
    }
}