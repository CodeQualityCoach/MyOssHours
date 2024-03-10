using MyOssHours.Backend.Domain.Core;

namespace MyOssHours.Backend.Domain.Projects;

public class ProjectHasNoOwnerException : MyOssHoursException
{
    public ProjectHasNoOwnerException() : base("At least one owner is required")
    {
    }
}