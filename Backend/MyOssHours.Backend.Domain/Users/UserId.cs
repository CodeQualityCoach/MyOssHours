using MyOssHours.Backend.Domain.ValueObjects;

namespace MyOssHours.Backend.Domain.Users;

/// <summary>
///     Value object for User
/// </summary>
public class UserId : EntityId
{
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