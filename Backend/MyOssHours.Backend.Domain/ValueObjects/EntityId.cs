using MyOssHours.Backend.Domain.Attributes;

namespace MyOssHours.Backend.Domain.ValueObjects;

/// <summary>
///     Abstract Value Object for all EntityId
/// </summary>
public abstract class EntityId : ValueObject
{
    protected EntityId()
    {
        Uuid = Guid.NewGuid();
    }

    protected EntityId(Guid uuid)
    {
        Uuid = uuid;
    }

    public Guid Uuid { get; }

    [CodeOfInterest("The operator allows an implicit conversion between an EntityId and a GUID")]
    public static implicit operator Guid(EntityId id)
    {
        return id.Uuid;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Uuid;
    }
}