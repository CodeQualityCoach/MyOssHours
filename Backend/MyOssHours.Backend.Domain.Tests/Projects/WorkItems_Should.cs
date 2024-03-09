using System.Reflection;
using FluentAssertions;
using MyOssHours.Backend.Domain.Projects;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace MyOssHours.Backend.Domain.Tests.Projects;

[TestFixture]
public class WorkItems_Should
{
    [Test(Description = "Verify that the constructor is private")]
    public void Have_No_Public_Constructors()
    {
        var constructor = typeof(WorkItem).GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);

        constructor.Should().BeNull();
    }

    [Test(Description = "Verify that create method works")]
    public void Be_Created_With_Create()
    {
        var project = new ProjectId();
        var workItem = WorkItem.Create(project, "Test Work Item", "Test Description", new List<ProjectHour>());

        workItem.Project.Should().Be(project);
        workItem.Name.Should().Be("Test Work Item");
        workItem.Description.Should().Be("Test Description");
        workItem.ProjectHours.Should().BeEmpty();
    }

    [Test(Description = "verify create works when project hours is null")]
    public void Be_Created_With_Create_When_ProjectHours_Is_Null()
    {
        var project = new ProjectId();
        var workItem = WorkItem.Create(project, "Test Work Item", "Test Description", null);

        workItem.Project.Should().Be(project);
        workItem.Name.Should().Be("Test Work Item");
        workItem.Description.Should().Be("Test Description");
        workItem.ProjectHours.Should().BeEmpty();
    }

    [Test(Description = "test that work item name cannot be null or empty")]
    public void Not_Be_Created_With_Create_When_Name_Is_Null()
    {
        var project = new ProjectId();
        Action act = () => WorkItem.Create(project, null, "Test Description", new List<ProjectHour>());

        act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'name')");
    }

    [Test(Description = "test that the description can be null")]
    public void Be_Created_With_Create_When_Description_Is_Null()
    {
        var project = new ProjectId();
        var workItem = WorkItem.Create(project, "Test Work Item", null, new List<ProjectHour>());

        workItem.Project.Should().Be(project);
        workItem.Name.Should().Be("Test Work Item");
        workItem.Description.Should().BeNull();
        workItem.ProjectHours.Should().BeEmpty();
    }

    [Test(Description = "test that the description can not be empty")]
    public void Not_Be_Created_With_Create_When_Description_Is_Empty()
    {
        var project = new ProjectId();
        Action act = () => WorkItem.Create(project, "Test Work Item", string.Empty, new List<ProjectHour>());

        act.Should().Throw<ArgumentException>().WithMessage("Value cannot be empty. (Parameter 'description')");
    }

    [Test(Description = "test that the project cannot be null")]
    public void Not_Be_Created_With_Create_When_Project_Is_Null()
    {
        Action act = () => WorkItem.Create(null, "Test Work Item", "Test Description", new List<ProjectHour>());

        act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'project')");
    }
}