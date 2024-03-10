using MyOssHours.Backend.Domain.Core;

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

    [CodeOfInterest(because: "This uses predicates/lambdas to verify on data outside of the aggregate root")]
    public static User Create(
        UserId id, string nickname, string email,
        Predicate<string> nicknameIsUnique, Predicate<string> emailIsUnique)
    {
        if (nickname is null)
            throw new ArgumentException("Nickname cannot be null or empty");
        if (email is null)
            throw new ArgumentException("Email cannot be null or empty");
        if (!nicknameIsUnique(nickname))
            throw new NicknameIsNotUniqueException(nickname);
        if (!emailIsUnique(email))
            throw new EmailIsNotUniqueException(email);

        return new User(id, nickname, email);
    }
}