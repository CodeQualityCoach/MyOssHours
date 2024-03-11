using MyOssHours.Backend.Domain.Projects;

namespace MyOssHours.Backend.Application.Abstractions;

public interface IProjectHoursRepository
{
    Task<ProjectHour> CreateProjectHour(ProjectHour projectHour);
}