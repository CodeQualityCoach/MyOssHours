using MyOssHours.Backend.Domain.Core;

namespace MyOssHours.Backend.Domain.Projects;

/// <summary>
///     Value Object for ProjectHour
/// </summary>
public class ProjectHourId : EntityId
{
    public static ProjectHourId Empty => new ProjectHourId(Guid.Empty);
    public ProjectHourId()
    {
    }

    public ProjectHourId(Guid uuid) : base(uuid)
    {
    }

    public static implicit operator ProjectHourId(Guid value)
    {
        return new ProjectHourId(value);
    }
}