using MyOssHours.Backend.Domain.Projects;
using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Application.Abstractions;

public interface IWorkItemsRepository
{
    Task<WorkItem> CreateWorkItem(WorkItem workItem);
    Task<IEnumerable<WorkItem>> GetWorkItems(UserId uuid, ProjectId project);
    Task<bool> DeleteWorkItem(UserId uuid, WorkItemId workItem);
}