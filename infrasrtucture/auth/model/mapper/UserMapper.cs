using AutoMapper;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.auth.model.view;

namespace PISWF.infrasrtucture.auth.model.mapper;

public class UserMapper : Mapper
{
    public UserMapper() : base(new MapperConfiguration(
        cfg =>
        {
            cfg.CreateMap<User, UserBasic>();
            cfg.CreateMap<UserBasic, User>();
            cfg.CreateMap<User, UserRich>();
            cfg.CreateMap<UserRich, User>();
            cfg.CreateMap<User, UserAuth>();
            cfg.CreateMap<UserAuth, User>();
        })
    ) { }

    public UserMapper(IConfigurationProvider configurationProvider) : base(configurationProvider) { }

    public override string ToString()
    {
        return "Маппер предназначен для авторизации";
    }
}