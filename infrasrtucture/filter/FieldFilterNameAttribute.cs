namespace PISWF.infrasrtucture.filter;

public class FieldFilterNameAttribute : Attribute
{
    public string Name { get; }

    public FieldFilterNameAttribute(string name)
    {
        Name = name;
    }
}