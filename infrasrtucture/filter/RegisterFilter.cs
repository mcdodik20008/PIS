using PISWF.domain.registermc.model.entity;

namespace PISWF.infrasrtucture.filter;

public class RegisterFilter : FilterModel<RegisterMC>
{
    public FilterField<int> Number;

    public FilterField<DateTime> ValidDate;

    public FilterField<int> Year;

    public FilterField<Double> Price;

    public RegisterFilter()
    {
        ValidDate = new FilterField<DateTime>(DateTime.Now, "");
    }

    public override Func<RegisterMC, bool> FilterExpression
    {
        get
        {
            // TODO: Доразобраться с филтром
            return p =>
            {
               /* var numberFlag = Number.action switch
                {
                    "Меньше" => p.Number < Number.value,
                    "Больше" => p.Number > Number.value,
                    "Равное" => p.Number == Number.value,
                    _ => throw new ArgumentException("Не опознанный тип филтрации")
                };
                return numberFlag;*/
               return true;
            };
        }
    }

    public override void Reset()
    {
        Number = null;
        ValidDate = null;
        Year = null;
        Price = null;
    }
}