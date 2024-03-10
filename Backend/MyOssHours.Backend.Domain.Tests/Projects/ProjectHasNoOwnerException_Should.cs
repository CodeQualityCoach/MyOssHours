using FluentAssertions;
using MyOssHours.Backend.Domain.Core;
using MyOssHours.Backend.Domain.Projects;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace MyOssHours.Backend.Domain.Tests.Projects;

[TestFixture]
internal class ProjectHasNoOwnerException_Should
{
    [Test]
    public void Inherit_From_MyOssHoursException()
    {
        var exception = new ProjectHasNoOwnerException();
        exception.Should().BeAssignableTo<MyOssHoursException>();
    }

    [Test(Description = "The message should be 'At least one owner is required'")]
    public void Have_The_Message_At_Least_One_Owner_Is_Required()
    {
        var exception = new ProjectHasNoOwnerException();
        exception.Message.Should().Be("At least one owner is required");
    }
}