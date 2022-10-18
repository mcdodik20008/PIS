using PISWF.infrasrtucture.auth.context.repository;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.auth.model.mapper;
using PISWF.infrasrtucture.auth.model.view;
using PISWF.infrasrtucture.guard;
using PISWF.infrasrtucture.page;

namespace PISWF.infrasrtucture.auth.service;

public class UserService
{
    private UserRepository UserRepository { get; }

    private RoleRepository RoleRepository { get; }

    private UserMapper UserMapper { get; }
    
    private ErrorQueue ErrorQueue { get; }
    
    public UserService(UserRepository userRepository, RoleRepository roleRepository, UserMapper userMapper, ErrorQueue errorQueue)
    {
        UserRepository = userRepository;
        RoleRepository = roleRepository;
        UserMapper = userMapper;
        ErrorQueue = errorQueue;
    }
    
    public User Authorization(string login, string password)
    {
        var userShort = new UserBasic(login, password);
        return Authorization(userShort);
    }
    
    public User Authorization(UserBasic userShort)
    {
       using var transaction = UserRepository.DataBase.BeginTransaction();
       {
           try
           {
               var hash = userShort.Password;
               var predicate = new Func<User, bool>(x => x.Login!.Equals(userShort.Login) && x.Password.Equals(hash));
               var user = UserRepository.Entity.Where(predicate).FirstOrDefault();
               transaction.Commit();
               return user ?? throw new UnauthorizedAccessException("Ошибка в логине или пароле");
               
           }
           catch(UnauthorizedAccessException exep)
           {
               ErrorQueue.Enqueue(exep);
               return null;
           }
       }
    
    }

    public List<UserBasic> Read(Page page)
    {
        var entity = UserRepository.Entity.Skip(page.Number * page.Size).Take(page.Size);
        return UserMapper.Map<List<UserBasic>>(entity);
    }

    public User Add(UserBasic userShort)
    {
        var user = UserMapper.Map<User>(userShort);
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
    
    public User Delete(UserBasic userS)
    {
        var user = UserMapper.Map<User>(userS);
        UserRepository.Entity.Remove(user);
        UserRepository.SaveChanges();
        return user;
    }
}