using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.muni_org.model.view;
using PISWF.infrasrtucture.muni_org.service;

namespace PISWF.infrasrtucture.muni_org.controller;

public class OrganizationController
{
    private OrganizationService OrganizationService { get; }
    
    private AuthController AuthController { get; }

    public OrganizationController(OrganizationService organizationService, AuthController authController)
    {
        OrganizationService = organizationService;
        AuthController = authController;
    }

    public List<OrganizationShort> Read()
    {
       return OrganizationService.GetAll();
    }
    
    public OrganizationShort Read(long id)
    {
        return null;//OrganizationService.GetById(id);
    }
    
    public OrganizationShort Add(OrganizationShort organizationShort)
    {
        return OrganizationService.Add(organizationShort);
    }
    
    public OrganizationShort Update(OrganizationShort organizationShort)
    {
        return OrganizationService.Update(organizationShort);
    }
    
    public OrganizationShort Delete(OrganizationShort organizationShort)
    {
        return OrganizationService.Delete(organizationShort);
    }
}