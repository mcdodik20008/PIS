namespace PISWF.infrasrtucture.filter;

public class FilterField<T>
{
    public T value;

    public string action;

    public FilterField(T value, string action)
    {
        this.value = value;
        this.action = action;
    }
}