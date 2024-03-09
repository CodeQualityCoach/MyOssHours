using System.Reflection;
using FluentAssertions;
using MyOssHours.Backend.Domain.Attributes;
using MyOssHours.Backend.Domain.Users;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace MyOssHours.Backend.Domain.Tests.Users;

[TestFixture]
public class CodeOfInterestAttribute_Should
{
    [Test(Description = "CodeOfInterestAttribute is an Attribute")]
    public void Be_An_Attribute()
    {
        typeof(CodeOfInterestAttribute).Should().BeAssignableTo<Attribute>();
    }
}

[TestFixture]
public class User_Should
{

    [Test(Description = "ProjectHour is an IAggreateRoot")]
    public void Be_An_IAggregateRoot()
    {
        typeof(User).Should().BeAssignableTo<IAggregateRoot>();
    }

    [Test(Description = "Verify that the constructor is private")]
    public void Have_No_Public_Constructors()
    {
        var constructor = typeof(User).GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);

        constructor.Should().BeNull();
    }

    [Test(Description = "Verify that the create method is public")]
    public void Have_A_Public_Create_Method()
    {
        var method = typeof(User).GetMethods(BindingFlags.Public | BindingFlags.Static);
        method.Count(m => m.Name == "Create").Should().BeGreaterThan(0);
    }

    [Test(Description = "Verify that create method works")]
    public void Be_Created_With_Create()
    {
        var user = User.Create(new UserId(), "Test User", "Test Email");
        user.Should().NotBeNull();
        user.Nickname.Should().Be("Test User");
        user.Email.Should().Be("Test Email");
    }

    [Test(Description = "test that user nickname cannot be null or empty")]
    public void Not_Be_Created_With_Create_When_Nickname_Is_Null()
    {
        Action act = () => User.Create(new UserId(), null!, "Test Email");
        act.Should().Throw<ArgumentException>().WithMessage("Nickname cannot be null or empty");
    }

    [Test(Description = "test that user email cannot be null or empty")]
    public void Not_Be_Created_With_Create_When_Email_Is_Null()
    {
        Action act = () => User.Create(new UserId(), "Test User", null!);
        act.Should().Throw<ArgumentException>().WithMessage("Email cannot be null or empty");
    }
}