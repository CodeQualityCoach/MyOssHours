using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Application.Abstractions;

public interface IUserRepository
{
    Task<User> EnsureUser(string email, string nickname);
}