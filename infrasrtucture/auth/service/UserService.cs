using Microsoft.EntityFrameworkCore;
using PISWF.infrasrtucture.auth.context.repository;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.auth.model.mapper;
using PISWF.infrasrtucture.auth.model.view;
using PISWF.infrasrtucture.page;

namespace PISWF.infrasrtucture.auth.service;

public class UserService
{
    private UserRepository UserRepository { get; }

    private RoleRepository RoleRepository { get; }

    private UserMapper UserMapper { get; }

    public UserService(UserRepository userRepository, RoleRepository roleRepository, UserMapper userMapper)
    {
        UserRepository = userRepository;
        RoleRepository = roleRepository;
        UserMapper = userMapper;
    }

    public User Authorization(string login, string password)
    {
        var userBasic = new UserAuth(login, password);
        return Authorization(userBasic);
    }

    public User Authorization(UserAuth userAuth)
    {
        var hash = userAuth.Password;
        var predicate = new Func<User, bool>(x => x.Login!.Equals(userAuth.Login) && x.Password.Equals(hash));
        var user = UserRepository.Entity
            .Include(x => x.Roles)
            .ThenInclude(y => y.Visibility)
            .Include(x => x.Organization)
            .Include(x => x.Municipality)
            .FirstOrDefault(predicate);
        return user ?? throw new UnauthorizedAccessException("Ошибка в логине или пароле");
    }

    public List<UserAuth> Read(Page page)
    {
        var entity = UserRepository.Entity.Skip(page.Number * page.Size).Take(page.Size);
        return UserMapper.Map<List<UserAuth>>(entity);
    }

    public User Add(UserBasic userBasic)
    {
        var user = UserMapper.Map<User>(userBasic);
        UserRepository.Entity.Add(user);
        UserRepository.SaveChanges();
        return user;
    }

    public User Update(User user)
    {
        UserRepository.Entity.Update(user);
        UserRepository.SaveChanges();
        return user;
    }

    public User Delete(UserBasic userBasic)
    {
        var user = UserMapper.Map<User>(userBasic);
        UserRepository.Entity.Remove(user);
        UserRepository.SaveChanges();
        return user;
    }
}