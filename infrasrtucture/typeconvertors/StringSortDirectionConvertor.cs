using AutoMapper;
using pis.infrasrtucture.sort;

namespace pis.infrasrtucture.mappertypeconvertor;

public class StringSortDirectionConvertor : ITypeConverter<string, SortDirection>
{
    public SortDirection Convert(string source, SortDirection direction, ResolutionContext context)
    {
        return source switch
        {
            "По возрастанию" => SortDirection.Up,
            "По убыванию" => SortDirection.Down,
            _ => SortDirection.None
        };
    }
}