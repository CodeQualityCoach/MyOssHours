namespace MyOssHours.Backend.Domain.Enumerations;

[Flags]
public enum PermissionLevel
{
    None = 0,
    Read = 1,
    Contribute = 2,
    Owner = 4
}