namespace MyOssHours.Backend.Domain.Exceptions;

public class MyOssHoursException : Exception
{
    public MyOssHoursException(string message) : base(message)
    {
    }
}