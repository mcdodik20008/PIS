namespace PISWF.infrasrtucture.filter;

public abstract class FilterModel
{
    public abstract void Reset();
}

public abstract class FilterModel<T>
{
    public abstract Func<T, bool> FilterExpression { get; }
}