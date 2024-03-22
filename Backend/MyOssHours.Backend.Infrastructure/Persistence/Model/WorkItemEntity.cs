using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOssHours.Backend.Infrastructure.Persistence.Model;

[Table("WorkItem")]
public class WorkItemEntity
{
    [Key]
    public long Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Uuid { get; set; }

    [MaxLength(128)]
    public required string Name { get; set; }

    [MaxLength(1024)]
    public string? Description { get; set; }

    public required ProjectEntity Project { get; set; }

    public virtual List<ProjectHourEntity> Hours { get; set; } = [];
}