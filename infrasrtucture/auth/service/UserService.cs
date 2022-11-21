using Microsoft.EntityFrameworkCore;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.auth.model.mapper;
using PISWF.infrasrtucture.auth.model.view;
using PISWF.infrasrtucture.page;

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

    public List<UserAuth> Read(Page page)
    {
        using var context = new AppDbContext();
        var entity = context.Users.Skip(page.Number * page.Size).Take(page.Size);
        return UserMapper.Map<List<UserAuth>>(entity);
    }

    public User Add(UserBasic userBasic)
    {
        using var context = new AppDbContext();
        var user = UserMapper.Map<User>(userBasic);
        context.Users.Add(user);
        context.SaveChanges();
        return user;
    }

    public User Update(User user)
    {
        using var context = new AppDbContext();
        context.Users.Update(user);
        context.SaveChanges();
        return user;
    }

    public User Delete(UserBasic userBasic)
    {
        using var context = new AppDbContext();
        var user = UserMapper.Map<User>(userBasic);
        context.Users.Remove(user);
        context.SaveChanges();
        return user;
    }
}