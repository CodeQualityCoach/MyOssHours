namespace MyOssHours.Backend.Presentation.Models;

public class UserModel
{
    public Guid Uuid { get; set; }
    public required string Nickname { get; set; }
}