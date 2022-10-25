using AutoMapper;

namespace pis.infrasrtucture.mappertypeconvertor;

public class StringDateTimeConvertor : ITypeConverter<string, DateTime>
{
    public DateTime Convert(string source, DateTime destination, ResolutionContext context)
    {
        return DateTime.Parse(source);
    }
}