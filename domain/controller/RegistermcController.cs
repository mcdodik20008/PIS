using System.Linq.Expressions;
using LinqKit;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.view;
using PISWF.domain.registermc.service;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.logger.controller;
using PISWF.infrasrtucture.page;

namespace PISWF.domain.registermc.controller;

public class RegistermcController
{
    private RegistermcService _registermcService;

    private AuthController _authController;

    private LogController _logController;

    public RegistermcController(RegistermcService registermcService, AuthController authController,
        LogController logController)
    {
        _authController = authController;
        _registermcService = registermcService;
        _logController = logController;
    }

    public List<RegisterMCShort> Read(
        Page page,
        Expression<Func<RegisterMC, bool>> filter,
        SortParameters sortParameters)
    {
        var user = _authController.AutorizedUser;
        var userFirstRole = user.Roles.FirstOrDefault();
        var predicate = PredicateBuilder.True<RegisterMC>().And(filter);
        if (userFirstRole is not null)
        {
            predicate = userFirstRole.Visibility.Rate switch
            {
                "Реестра" => predicate,
                "Муниципальный" => predicate.And(x => x.Municipality.Id.Equals(user.Municipality.Id)),
                "Организации" => predicate.And(x => x.Organization.Id.Equals(user.Organization.Id))
            };
        }

        return _registermcService.Read(page, predicate.Compile(), sortParameters);
    }

    public List<RegisterMCShort> Read(Page page)
    {
        return _registermcService.Read(page);
    }

    public RegisterMCLong Read(long id)
    {
        return _registermcService.Read(id);
    }

    public RegisterMCLong Create(RegisterMCLong view)
    {
        _logController.AddRecord("CreateRegisterMC", view);
        return _registermcService.Create(view);
    }

    public RegisterMCLong Update(long id, RegisterMCLong view)
    {
        _logController.AddRecord("UpdateRegisterMC", view);
        return _registermcService.Update(id, view);
    }

    public void Delete(long id)
    {
        var entity = _registermcService.Read(id);
        _registermcService.Delete(id);
        _logController.AddRecord("DeleteRegisterMC", entity);
    }

    public void ExportToExcel()
    {
        var user = _authController.AutorizedUser;
        var userFirstRole = user.Roles.FirstOrDefault();
        var predicate = PredicateBuilder.True<RegisterMC>();
        if (userFirstRole is not null)
        {
            predicate = userFirstRole.Visibility.Rate switch
            {
                "Реестра" => predicate,
                "Муниципальный" => predicate.And(x => x.Municipality.Id.Equals(user.Municipality.Id)),
                "Организации" => predicate.And(x => x.Organization.Id.Equals(user.Organization.Id))
            };
        }

        _registermcService.ExportToExcel(predicate.Compile());
    }

    public void UpLoadFile(RegisterMCLong registerMc)
    {
        var user = _authController.AutorizedUser;
        _registermcService.UpLoadFile(registerMc, user);
    }

    public void DeleteFile(long recordId, long fileId)
    {
        var entity = _registermcService.Read(recordId).Documents.Find(x => x.Id == fileId);
        _registermcService.DeleteFile(recordId, fileId);
        _logController.AddRecord("DeleteFile", entity);
    }
}