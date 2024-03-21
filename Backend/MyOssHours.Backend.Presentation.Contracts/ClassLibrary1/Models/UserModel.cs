namespace MyOssHours.Backend.Presentation.Contracts.Models;

public class UserModel
{
    public Guid Uuid { get; set; }
    public required string Nickname { get; set; }
}