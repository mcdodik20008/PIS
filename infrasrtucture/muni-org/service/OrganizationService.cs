using PISWF.infrasrtucture.muni_org.context.repository;
using PISWF.infrasrtucture.muni_org.model.entity;
using PISWF.infrasrtucture.muni_org.model.mapper;
using PISWF.infrasrtucture.muni_org.model.view;

namespace PISWF.infrasrtucture.muni_org.service;

public class OrganizationService
{
    private OrganizationRepository Repository { get; }

    private OrganizationMapper Mapper { get; }
    
    public OrganizationService(OrganizationRepository organizationRepository, OrganizationMapper municipalityMapper)
    {
        Repository = organizationRepository;
        Mapper = municipalityMapper;
    }
    
    public List<OrganizationShort> GetAll()
    {
        return Mapper.Map<List<OrganizationShort>>(Repository.Entity.ToList());
    }
    
    public OrganizationShort GetById(long id)
    {
        return Mapper.Map<OrganizationShort>(Repository.Entity.Find(id));
    }
    
    public OrganizationShort Add(OrganizationShort organizationShort)
    {
        var organization = Mapper.Map<Organization>(organizationShort);
        Repository.Entity.Add(organization);
        Repository.Save();
        return organizationShort;
    }
    
    public OrganizationShort Update(OrganizationShort organizationShort)
    {
        var organization = Mapper.Map<Organization>(organizationShort);
        Repository.Entity.Add(organization);
        Repository.Save();
        return organizationShort;
    }
    
    public OrganizationShort Delete(OrganizationShort organizationShort)
    {
        var organization = Mapper.Map<Organization>(organizationShort);
        Repository.Entity.Remove(organization);
        return organizationShort;
    }
}