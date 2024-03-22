using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Domain.Projects;

/// <summary>
///  WorkItem Domain Model which has WorkItems
/// </summary>
public class WorkItem
{
    private readonly List<ProjectHour> _projectHours = [];

    private WorkItem(
        WorkItemId id, ProjectId project,
        string name, string? description,
        IEnumerable<ProjectHour>? projectHours)
    {
        Uuid = id;
        Project = project;
        Name = name;
        Description = description;

        if (projectHours != null)
            _projectHours.AddRange(projectHours);
    }

    public WorkItemId Uuid { get; }
    public ProjectId Project { get; }
    public string Name { get; }
    public string? Description { get; }
    public IEnumerable<ProjectHour> ProjectHours => _projectHours.AsReadOnly();

    internal static WorkItem Create(ProjectId project, string name, string description, IEnumerable<ProjectHour>? projectHours = null)
    {
        return Create(new WorkItemId(), project, name, description, projectHours);
    }

    internal static WorkItem Create(WorkItemId id, ProjectId project, string name, string? description, IEnumerable<ProjectHour>? projectHours = null)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));
        if (project is null) throw new ArgumentNullException(nameof(project));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

        return new WorkItem(id, project, name, description, projectHours);
    }

    internal ProjectHour CreateProjectHour(WorkItemId workItem, UserId user, DateOnly date, TimeSpan duration, string? description)
    {
        var result = ProjectHour.Create(Uuid, user, date, duration, description);
        _projectHours.Add(result);
        return result;
    }

}