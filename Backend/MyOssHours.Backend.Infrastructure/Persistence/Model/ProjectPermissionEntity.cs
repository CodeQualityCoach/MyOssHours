using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyOssHours.Backend.Domain.Projects;

namespace MyOssHours.Backend.Infrastructure.Persistence.Model;

[Table("ProjectPermission")]
internal class ProjectPermissionEntity
{
    [Key]
    public long Id { get; set; }
    public PermissionLevel Role { get; set; }
    public virtual required UserEntity User { get; set; }
}