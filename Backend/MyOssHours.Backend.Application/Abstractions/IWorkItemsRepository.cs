using MyOssHours.Backend.Domain.Projects;
using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Application.Abstractions;

public interface IWorkItemsRepository
{
    Task<IEnumerable<WorkItem>> GetWorkItems(UserId uuid, ProjectId project);
    Task<bool> DeleteWorkItem(UserId uuid, WorkItemId workItem);
    Task<WorkItem> CreateWorkItem(ProjectId project, string name, string description);
}