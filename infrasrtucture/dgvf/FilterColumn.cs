namespace pis.infrasrtucture.dgvf;

public class FilterColumn
{
    public string Name { get; }
    
    public Type ValueType { get; }
    
    public string Value { get; set; }
    
    public string ValueComboBox { get; set; }
    
    public FilterColumn(string name, Type valueType, string value, string valueComboBox)
    {
        Name = name;
        ValueType = valueType;
        Value = value;
        ValueComboBox = valueComboBox;
    }
    public FilterColumn() { }
}