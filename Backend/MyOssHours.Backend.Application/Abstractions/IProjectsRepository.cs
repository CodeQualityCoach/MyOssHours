using MyOssHours.Backend.Domain.Projects;
using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Application.Abstractions;

public interface IProjectsRepository
{
    Task<IEnumerable<Project>> GetProjects(UserId currentUser, int offset = 0, int size = 20);
    Task<Project> GetProject(Guid uuid, UserId currentUser);
    Task<Project> CreateProject(Project project);
}