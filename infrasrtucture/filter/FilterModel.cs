namespace PISWF.infrasrtucture.filter;

public abstract class FilterModel
{
    public abstract void Reset();
}

public abstract class FilterModel<T> : FilterModel
{
    public abstract Func<T, bool> FilterExpression { get; }
}