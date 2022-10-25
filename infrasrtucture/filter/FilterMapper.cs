using AutoMapper;
using pis.infrasrtucture.mappertypeconvertor;

namespace PISWF.infrasrtucture.filter;

public class FilterMapper : Mapper
{
    public FilterMapper() : base(
        new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<string, Comparators>().ConvertUsing(new StringComparatorConvertor()); 
                cfg.CreateMap<string, int>().ConvertUsing(new StringIntConvertor());
                cfg.CreateMap<string, double>().ConvertUsing(new StringDoubleConvertor());
                cfg.CreateMap<string, DateTime>().ConvertUsing(new StringDateTimeConvertor());
                cfg.CreateMap<string, string>().ConvertUsing(new StringStringConvertor());
            }))
    {
    }

    public FilterMapper(IConfigurationProvider configuration) : base(configuration)
    {
        configuration.AssertConfigurationIsValid();
    }
}