using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.muni_org.model.view;
using PISWF.infrasrtucture.muni_org.service;

namespace PISWF.infrasrtucture.muni_org.controller;

public class MunicipalityController
{
    private MunicipalityService MunicipalityService { get; }
    
    private AuthController AuthController { get; }

    public MunicipalityController(MunicipalityService municipalityService, AuthController authController)
    {
        MunicipalityService = municipalityService;
        AuthController = authController;
    }

    public List<MunicipalityShort> Read()
    {
        return MunicipalityService.GetAll();
    }
    
    public MunicipalityShort Read(long id)
    {
        return MunicipalityService.GetById(id);
    }
    
    public MunicipalityShort Add(MunicipalityShort municipalityShort)
    {
        return MunicipalityService.Add(municipalityShort);
    }
    
    public MunicipalityShort Update(MunicipalityShort municipalityShort)
    {
        return MunicipalityService.Update(municipalityShort);
    }
    
    public MunicipalityShort Delete(MunicipalityShort municipalityShort)
    {
        return MunicipalityService.Delete(municipalityShort);
    }
}