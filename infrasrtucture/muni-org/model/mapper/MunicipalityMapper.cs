using AutoMapper;
using PISWF.infrasrtucture.muni_org.model.entity;
using PISWF.infrasrtucture.muni_org.model.view;

namespace PISWF.infrasrtucture.muni_org.model.mapper;

public class MunicipalityMapper : Mapper
{
    public MunicipalityMapper() : base(new MapperConfiguration(
        cfg =>
        {
            cfg.CreateMap<Municipality, MunicipalityShort>();
            cfg.CreateMap<MunicipalityShort, Municipality>();
        })
    )
    {
    }

    public MunicipalityMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
    {
    }

    public override string ToString()
    {
        return "Муниципальный маппер";
    }
}