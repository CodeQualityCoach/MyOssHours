namespace MyOssHours.Backend.Domain.Projects;

/// <summary>
///  WorkItem Domain Model which has WorkItems
/// </summary>
public class WorkItem
{
    private WorkItem(
        WorkItemId id, ProjectId project, 
        string name, string description,
        IEnumerable<ProjectHour>? projectHours)
    {
        Uuid = id;
        Project = project;
        Name = name;
        Description = description;
        ProjectHours = projectHours ?? Array.Empty<ProjectHour>();
    }

    public WorkItemId Uuid { get; }
    public ProjectId Project { get; }
    public string Name { get; }
    public string Description { get; }
    public IEnumerable<ProjectHour> ProjectHours { get; private set; }

    public static WorkItem Create(ProjectId project, string name, string description, IEnumerable<ProjectHour>? projectHours = null)
    {
        return Create(new WorkItemId(), project, name, description, projectHours);
    }

    public static WorkItem Create(WorkItemId id, ProjectId project, string name, string description, IEnumerable<ProjectHour>? projectHours = null)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));
        if (project is null) throw new ArgumentNullException(nameof(project));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

        return new WorkItem(id, project, name, description, projectHours);
    }
}