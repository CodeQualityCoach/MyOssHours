namespace MyOssHours.Backend.Presentation.Contracts;

[Flags]
public enum PermissionLevelModel
{
    None = 0,
    Read = 1,
    Contribute = 2,
    Owner = 4
}