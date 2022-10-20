namespace pis.infrasrtucture.dgvf;

public class FilterColumn
{
    public String Name { get; }
    
    public Type ValueType { get; }
    
    public string value { get; set; }
    
    public FilterColumn(string name, Type valueType, string value)
    {
        Name = name;
        ValueType = valueType;
        this.value = value;
    }
    public FilterColumn() { }

    public override string ToString()
    {
        return ValueType.ToString() switch
        {
            "System.Int32" => $"convert([{Name}], '{ValueType}') LIKE '%{value}%'",
            "System.String" => $"convert([{Name}], '{ValueType}') LIKE '%{value}%'",
            _ => throw new ArgumentException("Тип столбца не может быть филтрован")
        };
    }
}