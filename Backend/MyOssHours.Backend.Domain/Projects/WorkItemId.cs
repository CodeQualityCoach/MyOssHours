using MyOssHours.Backend.Domain.Core;

namespace MyOssHours.Backend.Domain.Projects;

/// <summary>
///     Value Object for WorkItem
/// </summary>
public class WorkItemId : EntityId
{
    public static WorkItemId Empty => new WorkItemId(Guid.Empty);
    public WorkItemId()
    {
    }

    public WorkItemId(Guid uuid) : base(uuid)
    {
    }

    [CodeOfInterest("The operator allows an implicit conversion between a GUID and project id")]
    public static implicit operator WorkItemId(Guid value)
    {
        return new WorkItemId(value);
    }
}