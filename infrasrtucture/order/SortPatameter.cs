using System.Reflection;

namespace PISWF.domain.registermc.service;

public class SortParameter
{
    public SortParameter(PropertyInfo property, bool isAscending)
    {
        Property = property;
        IsAscending = isAscending;
    }

    public PropertyInfo Property { get; }
    public bool IsAscending { get; }
}