using FluentAssertions;
using MyOssHours.Backend.Domain.Core;
using MyOssHours.Backend.Domain.Projects;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace MyOssHours.Backend.Domain.Tests.Projects;

[TestFixture]
internal class DuplicateWorkItemNameException_Should
{
    [Test]
    public void Inherit_From_MyOssHoursException()
    {
        var workItem = WorkItem.Create(new ProjectId(), "Test", "Test Description");
        var exception = new DuplicateWorkItemNameException(workItem);
        exception.Should().BeAssignableTo<MyOssHoursException>();
    }

    [Test(Description = "The message should be 'An item with the same name has already been added. Name: Test'")]
    [TestCase("Test")]
    [TestCase("Test2")]
    public void Have_The_Message_An_Item_With_The_Same_Name_Has_Already_Been_Added(string name)
    {
        var workItem = WorkItem.Create(new ProjectId(), name, "Test Description");
        var exception = new DuplicateWorkItemNameException(workItem);
        exception.Message.Should().Be($"An item with the same name has already been added. Name: {name}");
    }

    [Test(Description = "The message should be 'An item with the same name has already been added. Name: Test'")]
    public void Have_The_Message_Items_With_The_Same_Name_Has_Already_Been_Added()
    {
        var workItem1 = WorkItem.Create(new ProjectId(), "Test1", "Test Description");
        var workItem2 = WorkItem.Create(new ProjectId(), "Test2", "Test Description");
        var exception = new DuplicateWorkItemNameException(workItem1, workItem2);
        exception.Message.Should().Be($"An item with the same name has already been added. Name: Test1, Test2");
    }
}