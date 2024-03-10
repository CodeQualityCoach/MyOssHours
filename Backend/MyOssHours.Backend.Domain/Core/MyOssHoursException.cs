namespace MyOssHours.Backend.Domain.Core;

public class MyOssHoursException : Exception
{
    public MyOssHoursException(string message) : base(message)
    {
    }
}