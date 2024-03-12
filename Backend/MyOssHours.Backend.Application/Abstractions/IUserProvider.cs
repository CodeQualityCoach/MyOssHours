using MyOssHours.Backend.Domain.Users;

namespace MyOssHours.Backend.Application.Abstractions;

public interface IUserProvider
{
    User GetCurrentUser();
}