using MyOssHours.Backend.Domain.Core;

namespace MyOssHours.Backend.Domain.Projects;

public class DuplicateWorkItemNameException : MyOssHoursException
{
    private readonly WorkItem[] _workItem;

    public DuplicateWorkItemNameException(params WorkItem[] workItem)
        : base($"An item with the same name has already been added. Name: {string.Join(", ", workItem.Select(x => x.Name))}")
    {
        _workItem = workItem;
    }
}