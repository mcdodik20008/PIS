using System.Linq.Expressions;

namespace PISWF.infrasrtucture.filter;

public interface FilterModel
{
    void Reset();
}

public interface FilterModel<T> : FilterModel
{
    Expression<Func<T, bool>> FilterExpression();
}