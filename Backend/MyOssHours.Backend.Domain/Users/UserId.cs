using MyOssHours.Backend.Domain.Core;

namespace MyOssHours.Backend.Domain.Users;

/// <summary>
///     Value object for User
/// </summary>
public class UserId : EntityId
{
    public static UserId Empty => new UserId(Guid.Empty);
    public UserId()
    {
    }

    public UserId(Guid uuid) : base(uuid)
    {
    }

    public static implicit operator UserId(Guid value)
    {
        return new UserId(value);
    }
}