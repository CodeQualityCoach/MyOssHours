using MyOssHours.Backend.Domain.Core;

namespace MyOssHours.Backend.Domain.Projects;

public class DuplicateProjectNameException : MyOssHoursException
{
    private readonly string _name;

    public DuplicateProjectNameException(string name) 
        : base($"The project name '{name}' is not unique")
    {
        _name = name;
    }
}