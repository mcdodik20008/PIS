using AutoMapper;

namespace pis.infrasrtucture.mappertypeconvertor;

public class StringIntConvertor : ITypeConverter<string, int>
{
    public int Convert(string source, int destination, ResolutionContext context)
    {
        return int.Parse(source);
    }
}