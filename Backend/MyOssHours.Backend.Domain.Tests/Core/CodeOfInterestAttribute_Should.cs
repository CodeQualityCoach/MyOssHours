using FluentAssertions;
using MyOssHours.Backend.Domain.Core;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace MyOssHours.Backend.Domain.Tests.Core;

[TestFixture]
public class CodeOfInterestAttribute_Should
{
    [Test(Description = "CodeOfInterestAttribute is an Attribute")]
    public void Be_An_Attribute()
    {
        typeof(CodeOfInterestAttribute).Should().BeAssignableTo<Attribute>();
    }

    [Test(Description = "CodeOfInterestAttribute has a constructor that takes a string")]
    public void Have_A_Constructor_That_Takes_A_String()
    {
        var constructor = typeof(CodeOfInterestAttribute).GetConstructor(new[] { typeof(string) });
        constructor.Should().NotBeNull();
    }

    [Test(Description = "CodeOfInterestAttribute has a property Because")]
    public void Have_A_Property_Because()
    {
        var property = typeof(CodeOfInterestAttribute).GetProperty("Because");
        property.Should().NotBeNull();

        var attribute = new CodeOfInterestAttribute("Test");
        attribute.Because.Should().Be("Test");
    }
}