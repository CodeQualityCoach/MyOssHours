﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOssHours.Backend.Infrastructure.Model;

[Table("ProjectHour")]
internal class ProjectHourEntity
{
    public long Id { get; set; }
    public Guid Uuid { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeSpan Duration { get; set; }
    public required UserEntity User { get; set; }
    public required WorkItemEntity WorkItem { get; set; }
    [MaxLength(1024)]
    public string? Description { get; set; }
}