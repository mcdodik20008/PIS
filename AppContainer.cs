using LightInject;
using pis.infrasrtucture.filter.impl;
using PISWF.domain.model.validator;
using PISWF.domain.registermc.controller;
using PISWF.domain.registermc.model.mapper;
using PISWF.domain.registermc.service;
using PISWF.infrasrtucture;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.auth.model.mapper;
using PISWF.infrasrtucture.auth.service;
using PISWF.infrasrtucture.filter;
using PISWF.infrasrtucture.logger.controller;
using PISWF.infrasrtucture.logger.service;
using PISWF.infrasrtucture.muni_org.controller;
using PISWF.infrasrtucture.muni_org.model.mapper;
using PISWF.infrasrtucture.muni_org.service;
using PISWF.view;

namespace PISWF;

public class AppContainer : ServiceContainer
{
    public AppContainer()
    {
        this.Register<AppDbContext>();
        this.RegisterSingleton<AppContainer>();

        #region form
        this.RegisterSingleton<Auth>();
        this.RegisterSingleton<DGVs>();
        this.RegisterSingleton<DgvLong>();
        #endregion

        #region registermc
        this.RegisterSingleton<RegistermcController>();
        this.RegisterSingleton<RegistermcService>();
        this.RegisterSingleton<RegisterMcMapper>();
        this.RegisterSingleton<RegistermcValidator>();
        #endregion
        
        #region organization
        this.RegisterSingleton<OrganizationController>();
        this.RegisterSingleton<OrganizationService>();
        this.RegisterSingleton<OrganizationMapper>();
        #endregion
        
        #region municipality
        this.RegisterSingleton<MunicipalityController>();
        this.RegisterSingleton<MunicipalityService>();
        this.RegisterSingleton<MunicipalityMapper>();
        #endregion
        
        #region auth
        this.RegisterSingleton<AuthController>();
        this.RegisterSingleton<UserMapper>();
        this.RegisterSingleton<AuthController>();
        this.RegisterSingleton<UserService>();
        #endregion
        
        #region log
        this.RegisterSingleton<LogService>();
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