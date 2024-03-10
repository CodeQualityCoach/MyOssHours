using System.Diagnostics.CodeAnalysis;

namespace MyOssHours.Backend.Domain.Projects;

[Flags]
public enum PermissionLevel
{
    None = 0,
    Read = 1,
    Contribute = 2,
    Owner = 4
}