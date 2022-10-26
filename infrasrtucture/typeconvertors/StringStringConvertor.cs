using AutoMapper;

namespace pis.infrasrtucture.mappertypeconvertor;

public class StringStringConvertor : ITypeConverter<string, string>
{
    public string Convert(string source, string destination, ResolutionContext context)
    {
        return source;
    }
}