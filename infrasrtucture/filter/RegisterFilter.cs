using PISWF.domain.registermc.model.entity;

namespace PISWF.infrasrtucture.filter;

public class RegisterFilter : FilterModel<RegisterMC>
{
    public int? Number;

    public DateTime? ValidDate;
    
    public int? Year;

    public Double? Price;


    public RegisterFilter()
    {
        ValidDate = DateTime.Now;
    }

    public override Func<RegisterMC, bool> FilterExpression
    {
        get
        {
            return p => GetFilter(p);
        }
    }

    private bool GetFilter(RegisterMC registerMc1)
    {
        return true;
    }

    public override void Reset()
    {
        Number = null; ValidDate = null; Year = null; Price = null;
    }
}