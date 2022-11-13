﻿using LightInject;
using pis.infrasrtucture.filter.impl;
using PISWF.domain.model.validator;
using PISWF.domain.registermc.context.repository;
using PISWF.domain.registermc.controller;
using PISWF.domain.registermc.model.mapper;
using PISWF.domain.registermc.service;
using PISWF.infrasrtucture;
using PISWF.infrasrtucture.auth.context.repository;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.auth.model.mapper;
using PISWF.infrasrtucture.auth.service;
using PISWF.infrasrtucture.filter;
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
        this.RegisterSingleton<DgvFilter>();
        #endregion

        #region registermc
        this.RegisterSingleton<RegistermcController>();
        this.RegisterSingleton<RegistermcService>();
        this.RegisterSingleton<FileDocumentMapper>();
        this.RegisterSingleton<FileDocumentRepository>();
        this.RegisterSingleton<RegisterMcMapper>();
        this.RegisterSingleton<RegisterMcRepository>();
        this.RegisterSingleton<RegistermcValidator>();
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
        this.RegisterSingleton<LogController>();
        #endregion

        #region filters
        this.RegisterSingleton<IFilterFactory, FilterFactory>();
        this.RegisterSingleton<FilterFactory>();
        this.RegisterSingleton<RegisterFilter>();
        this.RegisterSingleton<FilterSorterMapper>();
        #endregion

        #region excel exporter
        
        this.RegisterSingleton<ExcelExporter>();
        
        #endregion
    }
}