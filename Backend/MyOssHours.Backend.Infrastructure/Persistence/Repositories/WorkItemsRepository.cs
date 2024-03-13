using Microsoft.EntityFrameworkCore;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Projects;
using MyOssHours.Backend.Domain.Users;
using MyOssHours.Backend.Infrastructure.Persistence.Model;

namespace MyOssHours.Backend.Infrastructure.Persistence.Repositories;

internal class WorkItemsRepository(MyOssHoursDbContext dbContext) : IWorkItemsRepository
{
    public async Task<IEnumerable<WorkItem>> GetWorkItems(UserId uuid, ProjectId projectId)
    {
        var p = await dbContext.Projects.Include(x => x.WorkItems).Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Uuid == projectId);
        var hasAccess = p.Members.Any(x => x.User.Uuid == uuid);
        if (!hasAccess)
            throw new UnauthorizedAccessException();

        var project = Project.Create(p.Uuid, p.Name, p.Description, new List<ProjectPermission>());
        p.WorkItems.ForEach(x => project.AddWorkItem(x.Uuid, x.Name, x.Description));

        return project.WorkItems;
    }

    public Task<bool> DeleteWorkItem(UserId uuid, WorkItemId workItem)
    {
        throw new NotImplementedException();
    }

    public async Task<WorkItem> CreateWorkItem(ProjectId projectId, string name, string description)
    {
        var p = dbContext.Projects.First(x => x.Uuid == projectId);
        var ph = new WorkItemEntity
        {
            Name = name,
            Description = description,
            Project = p
        };
        dbContext.WorkItems.Add(ph);
        await dbContext.SaveChangesAsync();

        var project = Project.Create(p.Uuid, p.Name, p.Description, new List<ProjectPermission>());
        var result = project.AddWorkItem(ph.Uuid, ph.Name, ph.Description);

        return result;
    }
}