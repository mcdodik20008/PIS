using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.muni_org.model.view;
using PISWF.infrasrtucture.muni_org.service;

namespace PISWF.infrasrtucture.muni_org.controller;

public class MunicipalityController
{
    private MunicipalityService OrganizationService { get; }
    
    private AuthController AuthController { get; }

    public MunicipalityController(MunicipalityService organizationService, AuthController authController)
    {
        OrganizationService = organizationService;
        AuthController = authController;
    }

    public List<MunicipalityShort> Read()
    {
        return OrganizationService.GetAll();
    }
    
    public MunicipalityShort Read(long id)
    {
        return OrganizationService.GetById(id);
    }
    
    public MunicipalityShort Add(MunicipalityShort municipalityShort)
    {
        return OrganizationService.Add(municipalityShort);
    }
    
    public MunicipalityShort Update(MunicipalityShort municipalityShort)
    {
        return OrganizationService.Update(municipalityShort);
    }
    
    public MunicipalityShort Delete(MunicipalityShort municipalityShort)
    {
        return OrganizationService.Delete(municipalityShort);
    }
}