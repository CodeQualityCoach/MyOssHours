
namespace MyOssHours.Backend.Presentation.Contracts.Models;

public class ProjectModel
{
    public Guid Uuid { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<ProjectPermissionModel> Members { get; set; } = [];
    public List<WorkItemModel> WorkItems { get; set; } = [];
}