using System.Reflection;
using FluentAssertions;
using MyOssHours.Backend.Domain.Projects;
using MyOssHours.Backend.Domain.Users;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace MyOssHours.Backend.Domain.Tests.Projects;

[TestFixture]
public class ProjectHours_Should
{
    [Test(Description = "ProjectHour is not an IAggreateRoot")]
    public void Not_Be_An_IAggregateRoot()
    {
        typeof(ProjectHour).Should().NotBeAssignableTo<IAggregateRoot>();
    }

    [Test(Description = "Verify that the constructor is private")]
    public void Have_No_Public_Constructors()
    {
        var constructor = typeof(ProjectHour).GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);

        constructor.Should().BeNull();
    }

    [Test(Description = "Verify that the create method is internal")]
    public void Have_An_Internal_Create_Method()
    {
        // internal methods can be found using BindingFlags.NonPublic
        var allMethods = typeof(ProjectHour).GetMethods(BindingFlags.NonPublic | BindingFlags.Static).Where(m => m.Name == "Create");
        allMethods.Count().Should().BeGreaterThan(0);

        // but no public ones
        var method = typeof(ProjectHour).GetMethods(BindingFlags.Public | BindingFlags.Static);
        method.Count(m => m.Name == "Create").Should().Be(0);
    }

    [Test(Description = "Verify that create method works")]
    public void Be_Created_With_Create()
    {
        var today = DateTime.Today;
        var workItem = new WorkItemId();
        var user = new UserId();
        var projectHour = ProjectHour.Create(workItem, user, today, TimeSpan.FromMinutes(60), "Test Description");

        projectHour.Uuid.Should().NotBe(ProjectHourId.Empty);
        projectHour.WorkItem.Should().Be(workItem);
        projectHour.User.Should().Be(user);
        projectHour.StartDate.Should().Be(today);
        projectHour.Duration.Should().Be(TimeSpan.FromMinutes(60));
        projectHour.Description.Should().Be("Test Description");
    }

    [Test(Description = "Verify that create method works with Id parameter")]
    public void Be_Created_With_Create_With_Id()
    {
        ProjectHourId id = Guid.NewGuid();
        var today = DateTime.Today;
        var workItem = new WorkItemId();
        var user = new UserId();
        var projectHour = ProjectHour.Create(id, workItem, user, today, TimeSpan.FromMinutes(60), "Test Description");

        projectHour.Uuid.Should().Be(id);
        projectHour.WorkItem.Should().Be(workItem);
        projectHour.User.Should().Be(user);
        projectHour.StartDate.Should().Be(today);
        projectHour.Duration.Should().Be(TimeSpan.FromMinutes(60));
        projectHour.Description.Should().Be("Test Description");
    }

    [Test(Description = "test that the work item cannot be null")]
    public void Not_Be_Created_With_Create_When_WorkItem_Is_Null()
    {
        var today = DateTime.Today;
        var user = new UserId();
        Action act = () => ProjectHour.Create(null, user, today, TimeSpan.FromMinutes(60), "Test Description");

        act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'workItem')");
    }

    [Test(Description = "test that the user cannot be null")]
    public void Not_Be_Created_With_Create_When_User_Is_Null()
    {
        var today = DateTime.Today;
        var workItem = new WorkItemId();
        Action act = () => ProjectHour.Create(workItem, null, today, TimeSpan.FromMinutes(60), "Test Description");

        act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'user')");
    }

    [Test(Description = "test that the start date cannot be more than 30 days in the future")]
    public void Not_Be_Created_With_Create_When_StartDate_Is_More_Than_30_Days_In_The_Future()
    {
        var today = DateTime.Today.AddDays(31);
        var workItem = new WorkItemId();
        var user = new UserId();
        Action act = () => ProjectHour.Create(workItem, user, today, TimeSpan.FromMinutes(60), "Test Description");

        act.Should().Throw<ArgumentException>().WithMessage("Start date cannot be more than 30 days in the future. (Parameter 'startDate')");
    }

    [Test(Description = "Test that description can be null")]
    public void Be_Created_With_Create_When_Description_Is_Null()
    {
        var today = DateTime.Today;
        var workItem = new WorkItemId();
        var user = new UserId();
        var projectHour = ProjectHour.Create(workItem, user, today, TimeSpan.FromMinutes(60), null);

        projectHour.Description.Should().BeNull();
    }

    [Test(Description = "Test that duration cannot be less than 1 minute")]
    public void Not_Be_Created_With_Create_When_Duration_Is_Less_Than_1_Minute()
    {
        var today = DateTime.Today;
        var workItem = new WorkItemId();
        var user = new UserId();
        Action act = () => ProjectHour.Create(workItem, user, today, TimeSpan.FromSeconds(59), "Test Description");

        act.Should().Throw<ArgumentException>().WithMessage("Duration cannot be less than 1 minute. (Parameter 'duration')");
    }
}