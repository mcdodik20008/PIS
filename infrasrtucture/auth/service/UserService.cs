using Microsoft.EntityFrameworkCore;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.auth.model.mapper;
using PISWF.infrasrtucture.auth.model.view;

namespace PISWF.infrasrtucture.auth.service;

public class UserService
{
    private UserMapper UserMapper { get; }

    public UserService(UserMapper userMapper)
    {
        UserMapper = userMapper;
    }

    public User Authorization(string login, string password)
    {
        var userBasic = new UserAuth(login, password);
        return Authorization(userBasic);
    }

    public User Authorization(UserAuth userAuth)
    {
        using var context = new AppDbContext();
        var hash = userAuth.Password;
        var predicate = new Func<User, bool>(x => x.Login!.Equals(userAuth.Login) && x.Password.Equals(hash));
        var user = context.Users
            .Include(x => x.Roles)
            .ThenInclude(y => y.Visibility)
            .Include(x => x.Roles)
            .ThenInclude(x => x.Possibility)
            .Include(x => x.Organization)
            .Include(x => x.Municipality)
            .FirstOrDefault(predicate);
        return user ?? throw new UnauthorizedAccessException("Ошибка в логине или пароле");
    }
}