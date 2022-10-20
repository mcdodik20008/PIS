using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.auth.model.view;
using PISWF.infrasrtucture.auth.service;
using PISWF.infrasrtucture.logger.controller;
using PISWF.infrasrtucture.page;

namespace PISWF.infrasrtucture.auth.controller;

public class AuthController
{
    public User AutorizedUser { get; private set; }

    private UserService UserService { get; }

    private LogController LogController { get; }

    public AuthController(UserService userService, LogController logController)
    {
        UserService = userService;
        LogController = logController;
        AutorizedUser = UserService.Authorization(new UserBasic("guest", "1234"));
    }

    public User Authorization(UserBasic userShort)
    {
        if (userShort.Login == "guest")
            throw new Exception("Придумать название");
        LogController.AddRecord("Авторизация", userShort);
        AutorizedUser = UserService.Authorization(userShort);
        return AutorizedUser;
    }

    public List<UserAuth> Read(Page page)
    {
        return UserService.Read(page);
    }
    
    public User Add(UserBasic userShort)
    {
        LogController.AddRecord("Добавление пользователя", userShort);
        return UserService.Add(userShort);
    }

    public User Update(User user)
    {
        return UserService.Update(user);
    }

    public User Delete(UserBasic userShort)
    {
        return UserService.Delete(userShort);
    }
}