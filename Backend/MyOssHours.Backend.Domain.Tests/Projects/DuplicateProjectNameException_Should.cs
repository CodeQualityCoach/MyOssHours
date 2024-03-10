using FluentAssertions;
using MyOssHours.Backend.Domain.Core;
using MyOssHours.Backend.Domain.Projects;
using NUnit.Framework;

namespace MyOssHours.Backend.Domain.Tests.Projects;

[TestFixture]
internal class DuplicateProjectNameException_Should
{
    [Test]
    public void Inherit_From_MyOssHoursException()
    {
        var exception = new DuplicateProjectNameException("Test");
        exception.Should().BeAssignableTo<MyOssHoursException>();
    }

    [Test(Description = "The message should be 'The project name 'x' is not unique'")]
    [TestCase("Test")]
    [TestCase("Test2")]
    public void Have_The_Message_The_Project_Name_Is_Not_Unique(string name)
    {
        var exception = new DuplicateProjectNameException(name);
        exception.Message.Should().Be($"The project name '{name}' is not unique");
    }
}