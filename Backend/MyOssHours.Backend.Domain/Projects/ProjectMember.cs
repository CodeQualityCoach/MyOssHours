using MyOssHours.Backend.Domain.Enumerations;
using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Domain.Projects;

public class ProjectMember
{
    private ProjectMember(UserId userId, PermissionLevel role)
    {
        UserId = userId;
        Role = role;
    }

    public UserId UserId { get; }
    public PermissionLevel Role { get; }

    // create method
    public static ProjectMember Create(UserId userId, PermissionLevel role)
    {
        return new ProjectMember(userId, role);
    }
}