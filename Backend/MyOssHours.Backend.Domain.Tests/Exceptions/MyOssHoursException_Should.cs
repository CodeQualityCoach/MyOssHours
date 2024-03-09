using FluentAssertions;
using MyOssHours.Backend.Domain.Exceptions;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace MyOssHours.Backend.Domain.Tests.Exceptions;

[TestFixture]
internal class MyOssHoursException_Should
{
    [Test]
    public void Inherit_From_Exception()
    {
        var exception = new MyOssHoursException("Test");
        exception.Should().BeAssignableTo<Exception>();
    }
}