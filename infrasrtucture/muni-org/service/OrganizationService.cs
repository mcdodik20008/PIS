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
}