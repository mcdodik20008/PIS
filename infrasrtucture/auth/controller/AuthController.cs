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
    

    public AuthController(UserService userService)
    {
        UserService = userService;
        AutorizedUser = UserService.Authorization("guest", "1234");
    }

    public User Authorization(UserAuth userAuth)
    {
        if (userAuth.Login == "guest")
            throw new Exception("Придумать название");
        AutorizedUser = UserService.Authorization(userAuth);
        return AutorizedUser;
    }

    public List<UserAuth> Read(Page page)
    {
        return UserService.Read(page);
    }
    
    public User Add(UserBasic userBasic)
    {
        return UserService.Add(userBasic);
    }

    public User Update(User user)
    {
        return UserService.Update(user);
    }

    public User Delete(UserBasic userBasic)
    {
        return UserService.Delete(userBasic);
    }
}