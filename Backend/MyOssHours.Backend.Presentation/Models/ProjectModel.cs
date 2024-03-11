
namespace MyOssHours.Backend.Presentation.Models;

public class ProjectModel
{
    public Guid Uuid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ProjectPermissionModel> Members { get; set; } = new List<ProjectPermissionModel>();
    public List<WorkItemModel> WorkItems { get; set; } = new List<WorkItemModel>();
}