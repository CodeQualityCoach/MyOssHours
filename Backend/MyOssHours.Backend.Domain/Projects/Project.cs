using System.Security.Cryptography.X509Certificates;
using MyOssHours.Backend.Domain.Core;

namespace MyOssHours.Backend.Domain.Projects;

/// <summary>
///     This is the domain model for the project
/// </summary>
public class Project : IAggregateRoot
{
    private Project(ProjectId uuid, string name, string? description)
    {
        Uuid = uuid;
        Name = name;
        Description = description;
        WorkItems = new List<WorkItem>();
        Permissions = new List<ProjectPermission>();
    }

    public ProjectId Uuid { get; }

    public string Name { get; }
    public string? Description { get; }
    public IEnumerable<WorkItem> WorkItems { get; private set; }
    public IEnumerable<ProjectPermission> Permissions { get; private set; }

    public static Project Create(
        string name, string? description,
        IEnumerable<ProjectPermission> members,
        IEnumerable<WorkItem>? workItems = null)
    {
        return Create(new ProjectId(), name, description, members, workItems);
    }

    public static Project Create(
        ProjectId id, string name, string? description,
        IEnumerable<ProjectPermission> members,
        IEnumerable<WorkItem>? workItems = null,
        Predicate<string>? nameIsUnique = null)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (nameIsUnique != null && !nameIsUnique(name))
            throw new DuplicateProjectNameException(name);
        var workItemsSafe = workItems?.ToArray() ?? Array.Empty<WorkItem>();

        var projectMembers = members as ProjectPermission[] ?? members.ToArray();
        if (projectMembers.FirstOrDefault(x => x.Role == PermissionLevel.Owner) == null)
            throw new ProjectHasNoOwnerException();
        WorkItemNameValidation(workItemsSafe);

        return new Project(id, name, description)
        {
            Permissions = projectMembers.ToArray(),
            WorkItems = workItemsSafe
        };
    }

    /// <summary>
    /// Validates the work items and throws an exception if there are validation errors
    /// </summary>
    /// <param name="workItems">List of work items</param>
    /// <exception cref="DuplicateWorkItemNameException">Thrown when the list contains two items with the same name</exception>
    [CodeOfInterest(because: "The code uses an anonymous type to reduce the select statement and return the count AND the work item")]
    private static void WorkItemNameValidation(WorkItem[] workItems)
    {
        var workItemNames = workItems
            // the distinct ensures that we don't count twice
            .DistinctBy(x => x.Name)
            // count all distinct names in the overall list
            .Select(x =>
                new
                {
                    x.Name,
                    WorkItem = x,
                    Count = workItems.Count(y => string.Compare(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase) == 0)
                })
            // we only need of there are any duplicates
            .Where(x => x.Count > 1).ToArray();

        if (workItemNames.Any())
            throw new DuplicateWorkItemNameException(workItemNames.Select(x => x.WorkItem).ToArray());
    }
}