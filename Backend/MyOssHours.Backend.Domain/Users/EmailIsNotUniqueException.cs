using MyOssHours.Backend.Domain.Core;

namespace MyOssHours.Backend.Domain.Users;

public class EmailIsNotUniqueException : MyOssHoursException
{
    private readonly string _email;

    public EmailIsNotUniqueException(string email)
        : base($"Email '{email}' is already in use")
    {
        _email = email;
    }
}