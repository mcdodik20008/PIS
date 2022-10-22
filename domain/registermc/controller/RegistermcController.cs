using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.view;
using PISWF.domain.registermc.service;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.page;

namespace PISWF.domain.registermc.controller;

public class RegistermcController
{
    private RegistermcService _registermcService;

    private AuthController _authController;

    public RegistermcController(RegistermcService registermcService, AuthController authController)
    {
        _authController = authController;
        _registermcService = registermcService;
    }

    public List<RegisterMCShort> Read(Page page, Func<RegisterMC, bool> filter)
    {
        return _registermcService.Read(page, filter);
    }

    public RegisterMCLong Read(long id)
    {
        return _registermcService.Read(id);
    }

    public RegisterMCShort Create(RegisterMCShort view)
    {
        return _registermcService.Create(view);
    }

    public RegisterMCLong Update(long id, RegisterMCLong view)
    {
        return _registermcService.Update(id, view);
    }

    public RegisterMCShort Delete(RegisterMCShort view)
    {
        return _registermcService.Delete(view);
    }

    public void AddFile()
    {
    }

    public void DeleteFile()
    {
    }
}