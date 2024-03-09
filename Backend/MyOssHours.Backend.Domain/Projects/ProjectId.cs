using MyOssHours.Backend.Domain.Attributes;
using MyOssHours.Backend.Domain.ValueObjects;

namespace MyOssHours.Backend.Domain.Projects;

/// <summary>
///     Value Object for WorkItem
/// </summary>
public class ProjectId : EntityId
{
    public ProjectId()
    {
    }

    public ProjectId(Guid uuid) : base(uuid)
    {
    }

    [CodeOfInterest("The operator allows an implicit conversion between a GUID and project id")]
    public static implicit operator ProjectId(Guid value)
    {
        return new ProjectId(value);
    }
}