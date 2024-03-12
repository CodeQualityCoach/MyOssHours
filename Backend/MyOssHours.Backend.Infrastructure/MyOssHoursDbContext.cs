using Microsoft.EntityFrameworkCore;
using MyOssHours.Backend.Infrastructure.Model;

namespace MyOssHours.Backend.Infrastructure;

internal class MyOssHoursDbContext(DbContextOptions<MyOssHoursDbContext> options) : DbContext(options)
{
    public required DbSet<ProjectEntity> Projects { get; set; }
    public required DbSet<ProjectHourEntity> ProjectHours { get; set; }
    public required DbSet<ProjectPermissionEntity> ProjectPermissions { get; set; }
    public required DbSet<WorkItemEntity> WorkItems { get; set; }
    public required DbSet<UserEntity> Users { get; set; }
}