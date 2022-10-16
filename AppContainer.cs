using LightInject;
using PISWF.infrasrtucture;
using PISWF.infrasrtucture.auth.context.repository;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.auth.model.mapper;
using PISWF.infrasrtucture.auth.service;
using PISWF.infrasrtucture.logger.context;
using PISWF.infrasrtucture.logger.controller;
using PISWF.infrasrtucture.logger.service;
using PISWF.view;

namespace PISWF;

public class AppContainer : ServiceContainer
{
    public AppContainer()
    {
        this.RegisterSingleton<AppDbContext>();
        this.RegisterSingleton<AppContainer>();

        #region form
        this.RegisterSingleton<DGVFilter>();
        #endregion

        #region auth
        this.RegisterSingleton<AuthController>();
        this.RegisterSingleton<UserMapper>();
        this.RegisterSingleton<AuthController>();
        this.RegisterSingleton<UserService>();
        this.RegisterSingleton<UserRepository>();
        this.RegisterSingleton<RoleRepository>();
        #endregion
        
        #region log
        this.RegisterSingleton<LogService>();
        this.RegisterSingleton<LogRepository>();
        this.RegisterSingleton<LogServiceAsync>();
        this.RegisterSingleton<LogController>();
        #endregion
    }
}