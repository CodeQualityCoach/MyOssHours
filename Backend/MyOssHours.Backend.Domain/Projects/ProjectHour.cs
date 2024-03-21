using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Domain.Projects;

public class ProjectHour
{
    private ProjectHour(ProjectHourId id, WorkItemId workItem, UserId user, DateOnly startDate, TimeSpan duration, string? description)
    {
        Uuid = id;
        WorkItem = workItem;
        User = user;
        StartDate = startDate;
        Duration = duration;
        Description = description;
    }

    public TimeSpan Duration { get; }
    public string? Description { get; }

    public DateOnly StartDate { get; }

    public UserId User { get; }

    public ProjectHourId Uuid { get; }

    public WorkItemId WorkItem { get; }

    internal static ProjectHour Create(ProjectHourId uuid, WorkItemId workItem, UserId user, DateOnly startDate, TimeSpan duration, string? description)
    {
        if (uuid is null) throw new ArgumentNullException(nameof(uuid));
        if (workItem is null) throw new ArgumentNullException(nameof(workItem));
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (startDate > DateOnly.FromDateTime(DateTime.Today.AddDays(30))) throw new ArgumentException("Start date cannot be more than 30 days in the future.", nameof(startDate));
        if (duration < TimeSpan.FromMinutes(1)) throw new ArgumentException("Duration cannot be less than 1 minute.", nameof(duration));

        return new ProjectHour(uuid, workItem, user, startDate, duration, description);
    }

    internal static ProjectHour Create(WorkItemId workItem, UserId user, DateOnly startDate, TimeSpan duration, string? description)
    {
        return Create(new ProjectHourId(), workItem, user, startDate, duration, description);
    }
}