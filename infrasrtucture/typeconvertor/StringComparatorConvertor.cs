using AutoMapper;
using PISWF.infrasrtucture.filter;

namespace pis.infrasrtucture.mappertypeconvertor;

public class StringComparatorConvertor : ITypeConverter<string, Comparators>
{
    public Comparators Convert(string source, Comparators destination, ResolutionContext context)
    {
        return source switch
        {
            "Равно" => Comparators.Equals,
            "Меньше" or "До" => Comparators.Less,
            "Больше" or "После" => Comparators.More,
            "Содержит" => Comparators.Like,
            _ => Comparators.None
        };
    }
}