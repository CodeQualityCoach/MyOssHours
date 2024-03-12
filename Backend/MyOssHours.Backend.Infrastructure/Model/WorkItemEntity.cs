using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOssHours.Backend.Infrastructure.Model;

[Table("WorkItem")]
internal class WorkItemEntity
{
    public long Id { get; set; }
    public Guid Uuid { get; set; }
    [MaxLength(128)]
    public required string Name { get; set; }
    [MaxLength(1024)]
    public required string Description { get; set; }
    public required ProjectEntity Project { get; set; }
    public virtual List<ProjectHourEntity> Hours { get; set; } = [];
}