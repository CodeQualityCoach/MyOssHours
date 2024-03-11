using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Application.Abstractions;

public interface IUserRepository
{
    Task<User> EnsureUser(string sid, string nickname, string email);
}

public interface IUserProvider
{
    User GetCurrentUser();
}
