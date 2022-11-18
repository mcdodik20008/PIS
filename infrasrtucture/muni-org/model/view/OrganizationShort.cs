namespace PISWF.infrasrtucture.muni_org.model.view;

public class OrganizationShort
{
    public long Id { get; set; }

    public string Name { get; set; }
    
    public override string ToString()
    {
        return Name;
    }
}