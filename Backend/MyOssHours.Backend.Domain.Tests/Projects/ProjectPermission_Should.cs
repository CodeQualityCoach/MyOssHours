using System.Reflection;
using FluentAssertions;
using MyOssHours.Backend.Domain.Core;
using MyOssHours.Backend.Domain.Projects;
using MyOssHours.Backend.Domain.Users;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace MyOssHours.Backend.Domain.Tests.Projects;

[TestFixture]
public class ProjectPermission_Should
{
    [Test(Description = "ProjectPermission is not an IAggreateRoot")]
    public void Not_Be_An_IAggregateRoot()
    {
        typeof(ProjectPermission).Should().NotBeAssignableTo<IAggregateRoot>();
    }

    [Test(Description = "Verify that the constructor is private")]
    public void Have_No_Public_Constructors()
    {
        var constructor = typeof(ProjectPermission).GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);

        constructor.Should().BeNull();
    }

    [Test(Description = "Verify that the create method is internal")]
    public void Have_An_Internal_Create_Method()
    {
        // internal methods can be found using BindingFlags.NonPublic
        var allMethods = typeof(ProjectPermission).GetMethods(BindingFlags.NonPublic | BindingFlags.Static).Where(m => m.Name == "Create");
        allMethods.Count().Should().BeGreaterThan(0);

        // but no public ones
        var method = typeof(ProjectPermission).GetMethods(BindingFlags.Public | BindingFlags.Static);
        method.Count(m => m.Name == "Create").Should().Be(0);
    }

    [Test(Description = "Verify that create method works")]
    public void Be_Created_With_Create()
    {
        var user = new UserId();
        var projectMember = ProjectPermission.Create(user, PermissionLevel.None);

        projectMember.Role.Should().Be(PermissionLevel.None);
        projectMember.UserId.Should().Be(user);
    }

    [Test(Description = "test that the user cannot be null")]
    public void Not_Be_Created_With_Create_When_User_Is_Null()
    {
        Action act = () => ProjectPermission.Create(null!, PermissionLevel.None);
        act.Should().Throw<ArgumentNullException>();
    }
}