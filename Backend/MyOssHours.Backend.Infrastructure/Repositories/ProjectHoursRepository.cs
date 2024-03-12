using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Projects;
using MyOssHours.Backend.Infrastructure.Model;

namespace MyOssHours.Backend.Infrastructure.Repositories;

internal class ProjectHoursRepository(MyOssHoursDbContext dbContext) : IProjectHoursRepository
{
    public async Task<ProjectHour> CreateProjectHour(ProjectHour projectHour)
    {
        var w = dbContext.WorkItems.First(x => x.Uuid == projectHour.WorkItem);
        var p = dbContext.Projects.First(x => x.Uuid == w.Project.Uuid);
        var u = dbContext.Users.First(x => x.Uuid == projectHour.User);
        var ph = new ProjectHourEntity
        {
            Uuid = projectHour.Uuid,
            WorkItem = w,
            User = u,
            StartDate = projectHour.StartDate,
            Duration = projectHour.Duration,
            Description = projectHour.Description
        };
        dbContext.ProjectHours.Add(ph);
        await dbContext.SaveChangesAsync();

        var project = Project.Create(p.Uuid, p.Name, p.Description, Array.Empty<ProjectPermission>(), null, s => true);
        var result = project.CreateProjectHour(w.Uuid, u.Uuid, projectHour.StartDate, projectHour.Duration, projectHour.Description);

        return result;
    }
}