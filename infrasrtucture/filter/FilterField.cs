using System.Linq.Expressions;
using AutoMapper;
using LinqKit;

namespace PISWF.infrasrtucture.filter;

public class FilterField<T> where T : IComparable
{
    public T value;

    public Comparators action;

    public FilterField(T value, Comparators action)
    {
        this.value = value;
        this.action = action;
    }

    public FilterField<T> UpdateFilter(Mapper mapper, string value, string action)
    {
        this.action = mapper.Map<Comparators>(action);
        this.value = mapper.Map<T>(value);
        return this;
    }

    public Expression<Func<Tent, bool>> GetPredicate<Tent>(string propName)
    {
        var predicate = PredicateBuilder.True<Tent>();
        var type = typeof(Tent).GetProperty(propName);
        if (Comparators.None != action)
        {
            predicate = action switch
            {
                Comparators.Equals => predicate.And(l => type.GetValue(l).Equals(value)),
                Comparators.Less => predicate.And(l => value.CompareTo(type.GetValue(l) as IComparable) == 1),
                Comparators.More => predicate.And(l => value.CompareTo(type.GetValue(l) as IComparable) == -1)
            };
        }

        return predicate;
    }
}