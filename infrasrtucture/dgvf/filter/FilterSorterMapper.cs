using AutoMapper;
using pis.infrasrtucture.mappertypeconvertor;
using pis.infrasrtucture.sort;

namespace PISWF.infrasrtucture.filter;

public class FilterSorterMapper : Mapper
{
    public FilterSorterMapper() : base(
        new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<string, Comparators>().ConvertUsing(new StringComparatorConvertor()); 
                cfg.CreateMap<string, SortDirection>().ConvertUsing(new StringSortDirectionConvertor()); 
                cfg.CreateMap<string, int>().ConvertUsing(new StringIntConvertor());
                cfg.CreateMap<string, double>().ConvertUsing(new StringDoubleConvertor());
                cfg.CreateMap<string, DateTime>().ConvertUsing(new StringDateTimeConvertor());
                cfg.CreateMap<string, string>().ConvertUsing(new StringStringConvertor());
            }))
    {
    }
}