using System.Reflection;
using FluentAssertions;
using MyOssHours.Backend.Domain.Enumerations;
using MyOssHours.Backend.Domain.Projects;
using MyOssHours.Backend.Domain.Users;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace MyOssHours.Backend.Domain.Tests.Projects;

[TestFixture]
public class Project_Should
{
    [Test(Description = "ProjectHour is an IAggreateRoot")]
    public void Be_An_IAggregateRoot()
    {
        typeof(Project).Should().BeAssignableTo<IAggregateRoot>();
    }

    [Test(Description = "Verify that the constructor is private")]
    public void Have_No_Public_Constructors()
    {
        var constructor = typeof(Project).GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);

        constructor.Should().BeNull();
    }

    [Test(Description = "Verify that create method works")]
    public void Be_Created_With_Create()
    {
        var project = Project.Create("Test Project", "Test Description", new [] { ProjectMember.Create(new UserId(), PermissionLevel.Owner) }, new List<WorkItem>());

        project.Name.Should().Be("Test Project");
        project.Description.Should().Be("Test Description");
        project.ProjectMembers.Should().HaveCount(1);
        project.WorkItems.Should().BeEmpty();
    }

    [Test(Description = "A project must contain an project member with role Owner when created")]
    public void Not_Be_Created_With_Create_When_Members_Does_Not_Contain_Owner()
    {
        var members = new List<ProjectMember>
        {
            ProjectMember.Create(new UserId(), PermissionLevel.Contribute) // at least non-owner
        };

        Action act = () => Project.Create("Test Project", "Test Description", members, new List<WorkItem>());

        act.Should().Throw<ArgumentException>().WithMessage("At least one owner is required");
    }

    [Test(Description = "verify create works when members or work items is null")]
    public void Be_Created_With_Create_When_Members_Or_WorkItems_Is_Null()
    {
        var project = Project.Create("Test Project", "Test Description", new[] { ProjectMember.Create(new UserId(), PermissionLevel.Owner) }, null);

        project.Name.Should().Be("Test Project");
        project.Description.Should().Be("Test Description");
        project.ProjectMembers.Should().BeEmpty();
        project.WorkItems.Should().BeEmpty();
    }

    [Test(Description = "test that project name cannot be null or empty")]
    public void Not_Be_Created_With_Create_When_Name_Is_Null()
    {
        Action act = () => Project.Create(null!, "Test Description", new[] { ProjectMember.Create(new UserId(), PermissionLevel.Owner) }, new List<WorkItem>());

        act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'name')");
    }

    [Test(Description = "test that the description can be null")]
    public void Be_Created_With_Create_When_Description_Is_Null()
    {
        var project = Project.Create("Test Project", null, new[] { ProjectMember.Create(new UserId(), PermissionLevel.Owner) }, new List<WorkItem>());

        project.Name.Should().Be("Test Project");
        project.Description.Should().BeNull();
        project.ProjectMembers.Should().BeEmpty();
        project.WorkItems.Should().BeEmpty();
    }

    [Test(Description = "test that the description can not be empty")]
    public void Not_Be_Created_With_Create_When_Description_Is_Empty()
    {
        Action act = () => Project.Create("Test Project", string.Empty, new List<ProjectMember>(), new List<WorkItem>());

        act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'description')");
    }
}