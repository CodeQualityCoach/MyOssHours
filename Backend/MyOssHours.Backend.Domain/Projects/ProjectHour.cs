using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Domain.Projects;

public class ProjectHour
{
    private ProjectHour(ProjectHourId id, WorkItemId workItem, UserId user, DateTime startDate, TimeSpan duration, string description)
    {
        Uuid = id;
        WorkItem = workItem;
        User = user;
        StartDate = startDate;
        Duration = duration;
        Description = description;
    }

    public TimeSpan Duration { get; }
    public string Description { get; }

    public DateTime StartDate { get; }

    public UserId User { get; }

    public ProjectHourId Uuid { get; }

    public WorkItemId WorkItem { get; }

    internal static ProjectHour Create(ProjectHourId uuid, WorkItemId workItem, UserId user, DateTime startDate, TimeSpan duration, string description)
    {
        return new ProjectHour(uuid, workItem, user, startDate, duration, description);
    }

    internal static ProjectHour Create(WorkItemId workItem, UserId user, DateTime startDate, TimeSpan duration, string description)
    {
        return Create(new ProjectHourId(), workItem, user, startDate, duration, description);
    }
}