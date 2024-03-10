using System.Reflection;
using FluentAssertions;
using MyOssHours.Backend.Domain.Core;
using MyOssHours.Backend.Domain.Users;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace MyOssHours.Backend.Domain.Tests.Users;

[TestFixture]
public class User_Should
{

    [Test(Description = "User is an IAggregateRoot")]
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
        var user = User.Create(new UserId(), "Test User", "Test Email", _ => true, _ => true);
        user.Should().NotBeNull();
        user.Uuid.Should().NotBe(UserId.Empty);
        user.Nickname.Should().Be("Test User");
        user.Email.Should().Be("Test Email");
    }

    [Test(Description = "Verify that create method works with Id parameter")]
    public void Be_Created_With_Create_With_UserId()
    {
        UserId userId = Guid.NewGuid();
        var user = User.Create(userId, "Test User", "Test Email", _ => true, _ => true);
        user.Should().NotBeNull();
        user.Uuid.Should().BeSameAs(userId);
        user.Nickname.Should().Be("Test User");
        user.Email.Should().Be("Test Email");
    }

    [Test(Description = "test that user nickname cannot be null or empty")]
    public void Not_Be_Created_With_Create_When_Nickname_Is_Null()
    {
        Action act = () => User.Create(new UserId(), null!, "Test Email", _ => true, _ => true);
        act.Should().Throw<ArgumentException>().WithMessage("Nickname cannot be null or empty");
    }

    [Test(Description = "test that user email cannot be null or empty")]
    public void Not_Be_Created_With_Create_When_Email_Is_Null()
    {
        Action act = () => User.Create(new UserId(), "Test User", null!, _ => true, _ => true);
        act.Should().Throw<ArgumentException>().WithMessage("Email cannot be null or empty");
    }

    [Test(Description = "throw Exception when email is not unique")]
    public void Not_Be_Created_With_Create_When_Email_Is_Not_Unique()
    {
        Action act = () => User.Create(new UserId(), "Test User", "Test Email", _ => true, _ => false);
        act.Should().Throw<EmailIsNotUniqueException>().WithMessage("Email 'Test Email' is already in use");
    }

    [Test(Description = "throw NicknameIsNotUniqueException when nickname is not unique")]
    public void Not_Be_Created_With_Create_When_Nickname_Is_Not_Unique()
    {
        Action act = () => User.Create(new UserId(), "Test User", "Test Email", _ => false, _ => true);
        act.Should().Throw<NicknameIsNotUniqueException>().WithMessage("Nickname 'Test User' is already in us");
    }

}