namespace MyOssHours.Backend.Domain.Exceptions;

public class ProjectHasNoOwnerException : MyOssHoursException
{
    public ProjectHasNoOwnerException() : base("At least one owner is required")
    {
    }
}