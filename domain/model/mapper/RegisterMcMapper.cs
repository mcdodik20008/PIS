using AutoMapper;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.view;
using PISWF.infrasrtucture.muni_org.model.entity;
using PISWF.infrasrtucture.muni_org.model.view;

namespace PISWF.domain.registermc.model.mapper;

public class RegisterMcMapper : Mapper
{
    public RegisterMcMapper() : base(new MapperConfiguration(
        cfg =>
        {
            cfg.CreateMap<RegisterMC, RegisterMCShort>();
            cfg.CreateMap<RegisterMC, RegisterMCLong>();
            cfg.CreateMap<RegisterMCLong, RegisterMC>();
            cfg.CreateMap<RegisterMCShort, RegisterMC>();
            cfg.CreateMap<Organization, OrganizationShort>();
            cfg.CreateMap<OrganizationShort, Organization>();
            cfg.CreateMap<Municipality, MunicipalityShort>();
            cfg.CreateMap<MunicipalityShort, Municipality>();
            cfg.CreateMap<FileDocument, FileDocumentShort>();
            cfg.CreateMap<FileDocumentShort, FileDocument>();
        })
    )
    {
    }

    public RegisterMcMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
    {
    }
}