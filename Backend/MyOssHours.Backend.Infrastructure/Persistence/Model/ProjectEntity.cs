using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOssHours.Backend.Infrastructure.Persistence.Model;

[Table("Project")]
internal class ProjectEntity
{
    [Key]
    public long Id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Uuid { get; set; }
    [MaxLength(128)]
    public required string Name { get; set; }
    [MaxLength(1024)]
    public string? Description { get; set; }

    public virtual List<WorkItemEntity> WorkItems { get; set; } = [];
    public virtual List<ProjectPermissionEntity> Members { get; set; } = [];
}