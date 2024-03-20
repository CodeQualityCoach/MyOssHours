using Microsoft.EntityFrameworkCore;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Projects;
using MyOssHours.Backend.Domain.Users;
using MyOssHours.Backend.Infrastructure.Persistence.Model;

namespace MyOssHours.Backend.Infrastructure.Persistence.Repositories;

internal class ProjectsRepository(MyOssHoursDbContext dbContext) : IProjectsRepository
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<IEnumerable<Project>> GetProjects(UserId currentUser, int offset = 0, int size = 20)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var projects = dbContext.Projects
            .Where(x => x.Members.Any(m => m.User.Uuid == currentUser.Uuid && m.Role > PermissionLevel.None))
            .Skip(offset)
            .Take(size)
            .ToList()
            .Select(x =>
                Project.Create(x.Uuid, x.Name, x.Description, Array.Empty<ProjectPermission>(), Array.Empty<WorkItem>(), s => true));

        return projects;
    }

    public async Task<Project> GetProject(Guid uuid, UserId currentUser)
    {
        var project = await dbContext.Projects
            .Include(x => x.Members)
            .ThenInclude(x => x.User)
            .Include(x => x.WorkItems)
            .ThenInclude(x => x.Hours)
            .FirstOrDefaultAsync(x => x.Uuid == uuid && x.Members.Any(m => m.User.Uuid == currentUser.Uuid && m.Role > PermissionLevel.None));
        if (project is null)
            throw new UnauthorizedAccessException();

        var result = Project.Create(project.Uuid, project.Name, project.Description, Array.Empty<ProjectPermission>(), Array.Empty<WorkItem>(), s => true);

        project.WorkItems.ForEach(x =>
        {
            result.AddWorkItem(x.Uuid, x.Name, x.Description);
            x.Hours.ForEach(y =>
            {
                result.CreateProjectHour(x.Uuid, y.User.Uuid, y.StartDate, y.Duration, y.Description);
            });
        });
        project.Members.ForEach(x => result.AddMember(x.User.Uuid, x.Role));

        return result;
    }

    public async Task<Project> CreateProject(Project project)
    {
        var projectEntity = new ProjectEntity
        {
            Uuid = project.Uuid,
            Name = project.Name,
            Description = project.Description
        };
        // add members
        foreach (var member in project.Permissions)
            projectEntity.Members.Add(new ProjectPermissionEntity
            {
                User = dbContext.Users.First(x => x.Uuid == member.UserId),
                Role = member.Role
            });
        dbContext.Projects.Add(projectEntity);
        await dbContext.SaveChangesAsync();
        return project;
    }
}

// User Repository