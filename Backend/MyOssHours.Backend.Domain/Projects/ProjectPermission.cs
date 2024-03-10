using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Domain.Projects;

public class ProjectPermission
{
    private ProjectPermission(UserId userId, PermissionLevel role)
    {
        UserId = userId;
        Role = role;
    }

    public UserId UserId { get; }
    public PermissionLevel Role { get; }

    // create method
    internal static ProjectPermission Create(UserId userId, PermissionLevel role)
    {
        if (userId is null) throw new ArgumentNullException(nameof(userId));

        return new ProjectPermission(userId, role);
    }
}