using System.Reflection;

namespace pis.infrasrtucture.dgvf;

public class SorterColumn
{
    public PropertyInfo Property { get; }
    
    public string ValueSorter { get; set; }
    
    public SorterColumn(PropertyInfo property)
    {
        Property = property;
    }
}