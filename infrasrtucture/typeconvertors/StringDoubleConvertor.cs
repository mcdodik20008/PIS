using AutoMapper;

namespace pis.infrasrtucture.mappertypeconvertor;

public class StringDoubleConvertor : ITypeConverter<string, double>
{
    public double Convert(string source, double destination, ResolutionContext context)
    {
        return double.TryParse(source, out var xx) ? xx : 0;
    }
}