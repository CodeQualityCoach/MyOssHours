using MyOssHours.Backend.Domain.Core;

namespace MyOssHours.Backend.Domain.Users;

public class NicknameIsNotUniqueException : MyOssHoursException
{
    private readonly string _nickname;

    public NicknameIsNotUniqueException(string nickname)
        : base($"Nickname '{nickname}' is already in us")
    {
        _nickname = nickname;
    }
}