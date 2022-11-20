using System.Reflection;
using pis.infrasrtucture.sort;

namespace PISWF.domain.registermc.service;

public class SortParameter
{
    public SortParameter(PropertyInfo property, SortDirection direction = SortDirection.None)
    {
        Property = property;
        Direction = direction;
    }

    public PropertyInfo Property { get; }
    public SortDirection Direction { get; }
}