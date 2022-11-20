using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.auth.model.view;
using PISWF.infrasrtucture.auth.service;

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
        if (userAuth.Login.Equals("guest"))
            throw new Exception("Придумать название");
        AutorizedUser = UserService.Authorization(userAuth);
        return AutorizedUser;
    }
}