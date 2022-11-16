using AutoMapper;
using PISWF.infrasrtucture.muni_org.model.entity;
using PISWF.infrasrtucture.muni_org.model.view;

namespace PISWF.infrasrtucture.muni_org.model.mapper;

public class OrganizationMapper : Mapper
{
    public OrganizationMapper() : base(new MapperConfiguration(
        cfg =>
        {
            cfg.CreateMap<Organization, OrganizationShort>();
            cfg.CreateMap<OrganizationShort, Organization>();
           // cfg.CreateMap<List<Organization>, List<OrganizationShort>>();
           // cfg.CreateMap<List<OrganizationShort>, List<Organization>>();
        })
    )
    {
    }

    public OrganizationMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
    {
    }

    public override string ToString()
    {
        return "Организационный маппер";
    }
}