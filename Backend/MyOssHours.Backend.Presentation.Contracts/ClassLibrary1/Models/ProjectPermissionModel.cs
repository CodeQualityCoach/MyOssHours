namespace MyOssHours.Backend.Presentation.Contracts.Models;

public class ProjectPermissionModel
{
    public PermissionLevelModel Role { get; set; }
    public Guid UserId { get; set; }
    public required string Nickname { get; set; }
}