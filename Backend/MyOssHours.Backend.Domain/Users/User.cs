namespace MyOssHours.Backend.Domain.Users;

/// <summary>
///     This is the domain model for a user
/// </summary>
public class User : IAggregateRoot
{
    private User(UserId uuid, string nickname, string email)
    {
        Uuid = uuid;
        Nickname = nickname;
        Email = email;
    }

    public UserId Uuid { get; }
    public string Nickname { get; }
    public string Email { get; }

    public static User Create(UserId id, string nickname, string email)
    {
        return new User(id, nickname, email);
    }
}