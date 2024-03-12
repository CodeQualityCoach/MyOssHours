using Microsoft.EntityFrameworkCore;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Users;
using MyOssHours.Backend.Infrastructure.Model;

namespace MyOssHours.Backend.Infrastructure.Repositories;

internal class UsersRepository(MyOssHoursDbContext dbContext) : IUserRepository
{
    public async Task<User> EnsureUser(string sid, string nickname, string email)
    {
        // todo validation. nickname, email are unique
        // find user in database and return if available
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Sid == sid);
        if (user != null) return User.Create(user.Uuid, user.Nickname, user.Email, s => true, s => true);

        // create user
        user = new UserEntity
        {
            Uuid = Guid.NewGuid(),
            Sid = sid,
            Nickname = nickname,
            Email = email
        };
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        // return user
        return User.Create(user.Uuid, user.Nickname, user.Email, s => true, s => true);
    }
}