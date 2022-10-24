using LinqKit;
using PISWF.domain.registermc.model.entity;

namespace PISWF.infrasrtucture.filter;

public class RegisterFilter : FilterModel<RegisterMC>
{
    public FilterField<int> NumberField = new(0, "");

    public FilterField<DateTime> ValidDateField= new(DateTime.Now, "");

    public FilterField<int> YearField= new(0, "");

    public FilterField<double> PriceField= new(0, "");

    public RegisterFilter()
    {
        ValidDateField = new FilterField<DateTime>(DateTime.Now, "");
    }

    [Obsolete("Obsolete")]
    public override Func<RegisterMC, bool> FilterExpression
    {
        get
        {
            var predicate = PredicateBuilder.True<RegisterMC>()
                .And(NumberField.GetPredicate<RegisterMC>("Number"))
                .And(ValidDateField.GetPredicate<RegisterMC>("ValidDate"))
                .And(YearField.GetPredicate<RegisterMC>("Year"))
                .And(PriceField.GetPredicate<RegisterMC>("Price"))
                .Compile();
            return predicate;
        }
    }

    public override void Reset()
    {
        NumberField = new FilterField<int>(0, "");
        ValidDateField = new FilterField<DateTime>(DateTime.Now, "");
        YearField = new FilterField<int>(0, "");
        PriceField = new FilterField<double>(0, "");
    }
}