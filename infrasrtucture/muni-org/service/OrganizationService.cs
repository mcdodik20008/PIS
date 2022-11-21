using PISWF.infrasrtucture.muni_org.model.entity;
using PISWF.infrasrtucture.muni_org.model.mapper;
using PISWF.infrasrtucture.muni_org.model.view;

namespace PISWF.infrasrtucture.muni_org.service;

public class OrganizationService
{
    private OrganizationMapper Mapper { get; }

    public OrganizationService(OrganizationMapper municipalityMapper)
    {
        Mapper = municipalityMapper;
    }

    public List<OrganizationShort> GetAll()
    {
        using var context = new AppDbContext();
        return Mapper.Map<List<OrganizationShort>>(context.Organizations.ToList());
    }

    public Organization GetById(long id)
    {
        using var context = new AppDbContext();
        return context.Organizations.Find(id);
    }

    public OrganizationShort Add(OrganizationShort organizationShort)
    {
        using var context = new AppDbContext();
        var organization = Mapper.Map<Organization>(organizationShort);
        context.Organizations.Add(organization);
        context.SaveChanges();
        return organizationShort;
    }

    public OrganizationShort Update(OrganizationShort organizationShort)
    {
        using var context = new AppDbContext();
        var organization = Mapper.Map<Organization>(organizationShort);
        context.Organizations.Add(organization);
        context.SaveChanges();
        return organizationShort;
    }

    public OrganizationShort Delete(OrganizationShort organizationShort)
    {
        using var context = new AppDbContext();
        var organization = Mapper.Map<Organization>(organizationShort);
        context.Organizations.Remove(organization);
        return organizationShort;
    }
}