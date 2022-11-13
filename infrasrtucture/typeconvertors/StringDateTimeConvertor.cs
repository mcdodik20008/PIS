using AutoMapper;

namespace pis.infrasrtucture.mappertypeconvertor;

public class StringDateTimeConvertor : ITypeConverter<string, DateTime>
{
    // TODO: Плохо?
    public DateTime Convert(string source, DateTime destination, ResolutionContext context)
    {
        return DateTime.TryParse(source, out var xx) ? xx : DateTime.Now;
    }
}