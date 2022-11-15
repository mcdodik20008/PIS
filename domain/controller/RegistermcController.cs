using System.Linq.Expressions;
using LinqKit;
using PISWF.domain.model.validator;
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

    public void UpLoadFile(RegisterMC registerMc)
    {
        var user = _authController.AutorizedUser;
        _registermcService.UpLoadFile(registerMc, user);
    }

    public void DownLoadFile(FileDocumentShort doc)
    {
        _registermcService.DownLoadFile(doc);
    }
}