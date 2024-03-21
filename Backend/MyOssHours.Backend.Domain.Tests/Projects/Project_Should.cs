using System.Reflection;
using FluentAssertions;
using MyOssHours.Backend.Domain.Core;
using MyOssHours.Backend.Domain.Projects;
using MyOssHours.Backend.Domain.Users;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace MyOssHours.Backend.Domain.Tests.Projects;

[TestFixture]
public class Project_Should
{
    [Test(Description = "Project should implement IAggregateRoot")]
    public void Be_An_IAggregateRoot()
    {
        typeof(Project).Should().Implement<IAggregateRoot>();
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
        var project = Project.Create("Test Project", "Test Description", new[] { ProjectPermission.Create(new UserId(), PermissionLevel.Owner) }, new List<WorkItem>());

        project.Name.Should().Be("Test Project");
        project.Uuid.Should().NotBe(ProjectId.Empty);
        project.Description.Should().Be("Test Description");
        project.Permissions.Should().HaveCount(1);
        project.WorkItems.Should().BeEmpty();
    }

    [Test(Description = "Verify that create method works with Id parameter")]
    public void Be_Created_With_Create_When_Id_Is_Specified()
    {
        ProjectId id = Guid.NewGuid();
        var project = Project.Create(id, "Test Project", "Test Description", new[] { ProjectPermission.Create(new UserId(), PermissionLevel.Owner) }, new List<WorkItem>());

        project.Uuid.Should().Be(id);
    }

    [Test(Description = "A project must contain an project member with role Owner when created")]
    public void Not_Be_Created_With_Create_When_Members_Does_Not_Contain_Owner()
    {
        var members = new List<ProjectPermission>
        {
            ProjectPermission.Create(new UserId(), PermissionLevel.Contribute) // at least non-owner
        };

        Action act = () => Project.Create("Test Project", "Test Description", members, new List<WorkItem>());

        act.Should().Throw<ProjectHasNoOwnerException>().WithMessage("At least one owner is required");
    }

    [Test(Description = "verify create works when members or work items is null")]
    public void Be_Created_With_Create_When_Members_Or_WorkItems_Is_Null()
    {
        var project = Project.Create("Test Project", "Test Description", new[] { ProjectPermission.Create(new UserId(), PermissionLevel.Owner) }, null);

        project.Name.Should().Be("Test Project");
        project.Description.Should().Be("Test Description");
        project.Permissions.Should().HaveCount(1);
        project.WorkItems.Should().BeEmpty();
    }

    [Test(Description = "test that project name cannot be null or empty")]
    public void Not_Be_Created_With_Create_When_Name_Is_Null()
    {
        Action act = () => Project.Create(null!, "Test Description", new[] { ProjectPermission.Create(new UserId(), PermissionLevel.Owner) }, new List<WorkItem>());

        act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'name')");
    }

    [Test(Description = "test that the description can be null")]
    public void Be_Created_With_Create_When_Description_Is_Null()
    {
        var project = Project.Create("Test Project", null, new[] { ProjectPermission.Create(new UserId(), PermissionLevel.Owner) }, new List<WorkItem>());

        project.Name.Should().Be("Test Project");
        project.Description.Should().BeNull();
        project.Permissions.Should().HaveCount(1);
        project.WorkItems.Should().BeEmpty();
    }

    [Test(Description = "test that a work item name must be unique in a project")]
    public void Not_Be_Created_With_Create_When_WorkItem_Has_Duplicate_Names()
    {
        var projectId = new ProjectId();
        var workItems = new List<WorkItem>
        {
            WorkItem.Create(projectId, "Test Work Item", "Test Description")
        };

        Action act = () => Project.Create("Test Project", "Test Description", new[] { ProjectPermission.Create(new UserId(), PermissionLevel.Owner) }, workItems.Concat(workItems));

        act.Should().Throw<DuplicateWorkItemNameException>().WithMessage("An item with the same name has already been added. Name: Test Work Item");
    }

    [Test(Description = "Project name should be unique")]
    public void Not_Be_Created_With_Create_When_Name_Is_Duplicate()
    {
        var projectId = new ProjectId();

        Action act = () => Project.Create(projectId, "Test Project", "Test Description", new[] { ProjectPermission.Create(new UserId(), PermissionLevel.Owner) }, new List<WorkItem>(), _ => false);

        act.Should().Throw<DuplicateProjectNameException>().WithMessage("The project name 'Test Project' is not unique");
    }
}