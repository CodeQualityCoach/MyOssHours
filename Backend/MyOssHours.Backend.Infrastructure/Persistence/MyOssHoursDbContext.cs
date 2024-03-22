using Microsoft.EntityFrameworkCore;
using MyOssHours.Backend.Infrastructure.Persistence.Model;

namespace MyOssHours.Backend.Infrastructure.Persistence;

public class MyOssHoursDbContext(DbContextOptions<MyOssHoursDbContext> options) : DbContext(options)
{
    public required DbSet<ProjectEntity> Projects { get; set; }
    public required DbSet<ProjectHourEntity> ProjectHours { get; set; }
    public required DbSet<ProjectPermissionEntity> ProjectPermissions { get; set; }
    public required DbSet<WorkItemEntity> WorkItems { get; set; }
    public required DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //    optionsBuilder.UseSqlite();
    }
}