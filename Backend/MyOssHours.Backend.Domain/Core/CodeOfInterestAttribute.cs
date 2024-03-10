namespace MyOssHours.Backend.Domain.Core;

[AttributeUsage(AttributeTargets.All)]
public class CodeOfInterestAttribute : Attribute
{
    public CodeOfInterestAttribute(string because)
    {
        Because = because;
    }

    public string Because { get; }
}