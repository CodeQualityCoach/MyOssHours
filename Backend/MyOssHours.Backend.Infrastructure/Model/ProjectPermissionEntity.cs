using System.ComponentModel.DataAnnotations.Schema;
using MyOssHours.Backend.Domain.Projects;

namespace MyOssHours.Backend.Infrastructure.Model;

[Table("ProjectPermission")]
internal class ProjectPermissionEntity
{
    public long Id { get; set; }
    public PermissionLevel Role { get; set; }
    public virtual required UserEntity User { get; set; }
}