using MyOssHours.Backend.Domain.Projects;

namespace MyOssHours.Backend.Presentation.Models;

public class ProjectPermissionModel
{
    public PermissionLevel Role { get; set; }
    public Guid UserId { get; set; }
    public required string Nickname { get; set; }
}