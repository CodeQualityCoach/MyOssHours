using FluentAssertions;
using MyOssHours.Backend.Domain.Core;
using MyOssHours.Backend.Domain.Users;
using NUnit.Framework;

namespace MyOssHours.Backend.Domain.Tests.Users;

[TestFixture]
public class EmailIsNotUniqueException_Should
{
    [Test(Description = "EmailIsNotUniqueException is an MyOssHoursException")]
    public void Be_An_Exception()
    {
        typeof(EmailIsNotUniqueException).Should().BeAssignableTo<MyOssHoursException>();
    }

    [Test(Description = "Verify that the email is in the message")]
    public void Have_Email_In_Message()
    {
        var exception = new EmailIsNotUniqueException("Test Email");
        exception.Message.Should().Contain("Test Email");
    }
}