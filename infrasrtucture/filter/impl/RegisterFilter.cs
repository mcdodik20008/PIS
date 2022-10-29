using System.Linq.Expressions;
using LinqKit;
using PISWF.domain.registermc.model.entity;
using PISWF.infrasrtucture.filter;

namespace pis.infrasrtucture.filter.impl;

public class RegisterFilter : FilterModel<RegisterMC>
{
    [FieldFilterName("Number")] public FilterField<int> NumberField { get; set; } = new(0, Comparators.None);

    [FieldFilterName("ValidDate")]
    public FilterField<DateTime> ValidDateField { get; set; } = new(DateTime.Now, Comparators.None);

    [FieldFilterName("Year")] public FilterField<int> YearField { get; set; } = new(0, Comparators.None);

    [FieldFilterName("Price")] public FilterField<double> PriceField { get; set; } = new(0, Comparators.None);

    public Expression<Func<RegisterMC, bool>> FilterExpression()
    {
        var predicate = PredicateBuilder.True<RegisterMC>()
            .And(NumberField.GetPredicate<RegisterMC>("Number"))
            .And(ValidDateField.GetPredicate<RegisterMC>("ValidDate"))
            .And(YearField.GetPredicate<RegisterMC>("Year"))
            .And(PriceField.GetPredicate<RegisterMC>("Price"));
        return predicate;
    }

    public void Reset()
    {
        NumberField = new FilterField<int>(0, Comparators.None);
        ValidDateField = new FilterField<DateTime>(DateTime.Now, Comparators.None);
        YearField = new FilterField<int>(0, Comparators.None);
        PriceField = new FilterField<double>(0, Comparators.None);
    }
}