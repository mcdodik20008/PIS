using LinqKit;
using PISWF.domain.registermc.model.entity;
using PISWF.infrasrtucture.filter;

namespace pis.infrasrtucture.filter.impl;

public class RegisterFilter : FilterModel<RegisterMC>
{
    public FilterField<int> NumberField = new(0, Comparators.None);

    public FilterField<DateTime> ValidDateField = new(DateTime.Now, Comparators.None);

    public FilterField<int> YearField = new(0, Comparators.None);

    public FilterField<double> PriceField = new(0, Comparators.None);

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
        NumberField = new FilterField<int>(0, Comparators.None);
        ValidDateField = new FilterField<DateTime>(DateTime.Now, Comparators.None);
        YearField = new FilterField<int>(0, Comparators.None);
        PriceField = new FilterField<double>(0, Comparators.None);
    }
}