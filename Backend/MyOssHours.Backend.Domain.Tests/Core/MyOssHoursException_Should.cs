using FluentAssertions;
using MyOssHours.Backend.Domain.Core;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace MyOssHours.Backend.Domain.Tests.Core;

[TestFixture]
internal class MyOssHoursException_Should
{
    [Test]
    public void Inherit_From_Exception()
    {
        var exception = new MyOssHoursException("Test");
        exception.Should().BeAssignableTo<Exception>();
        exception.Message.Should().Be("Test");
    }
}