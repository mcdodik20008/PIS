using AutoMapper;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.view;

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
        })
    )
    {
    }

    public RegisterMcMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
    {
    }
}