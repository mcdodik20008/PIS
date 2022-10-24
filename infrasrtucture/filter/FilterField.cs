using System.Linq.Expressions;
using LinqKit;

namespace PISWF.infrasrtucture.filter;

public class FilterField<T> where T : IComparable
{
    public T value;

    public string action;

    public FilterField(T value, string action)
    {
        this.value = value;
        this.action = action;
    }

    public Expression<Func<Tent, bool>> GetPredicate<Tent>(string propName)
    {
        var predicate = PredicateBuilder.True<Tent>();
        var type = typeof(Tent).GetProperty(propName);
        if (!action.Equals(""))
        {
            predicate = action switch
            {
                "Равно" => predicate.And(l => type.GetValue(l).Equals(value)),
                "Меньше" or "До" => predicate.And(l => value.CompareTo(type.GetValue(l) as IComparable) == 1),
                "Больше" or "После" => predicate.And(l => value.CompareTo(type.GetValue(l) as IComparable) == -1),
            };
        }
        return predicate;
    }
}