using MyOssHours.Backend.Domain.Core;

namespace MyOssHours.Backend.Domain.Projects;

public class WorkItemNotFoundException(WorkItemId workItem)
    : MyOssHoursException($"Work item with id {workItem} was not found");