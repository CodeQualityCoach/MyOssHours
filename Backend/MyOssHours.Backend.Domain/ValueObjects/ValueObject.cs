﻿using System.Diagnostics.CodeAnalysis;
using MyOssHours.Backend.Domain.Attributes;

namespace MyOssHours.Backend.Domain.ValueObjects;

/// <summary>
///     ValueObject is a class from the DDD book by Eric Evans
/// </summary>
[ExcludeFromCodeCoverage()]
[CodeOfInterest("Implements a value object according to the DDD book from Eric Evans")]
public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject? right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null)) return false;

        return ReferenceEquals(left, right) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject? right)
    {
        return !EqualOperator(left, right);
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType()) return false;

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x.GetHashCode())
            .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObject one, ValueObject two)
    {
        return EqualOperator(one, two);
    }

    public static bool operator !=(ValueObject one, ValueObject two)
    {
        return NotEqualOperator(one, two);
    }
}