using FluentAssertions;
using MyOssHours.Backend.Domain.Core;
using MyOssHours.Backend.Domain.Users;
using NUnit.Framework;

namespace MyOssHours.Backend.Domain.Tests.Users;

[TestFixture]
public class NicknameIsNotUniqueException_Should
{
    [Test(Description = "NicknameIsNotUniqueException is an MyOssHoursException")]
    public void Be_An_Exception()
    {
        typeof(NicknameIsNotUniqueException).Should().BeAssignableTo<MyOssHoursException>();
    }

    [Test(Description = "Verify that the nickname is in the message")]
    public void Have_Nickname_In_Message()
    {
        var exception = new NicknameIsNotUniqueException("Test Nickname");
        exception.Message.Should().Contain("Test Nickname");
    }
}