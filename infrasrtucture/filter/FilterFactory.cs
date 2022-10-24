using LightInject;

namespace PISWF.infrasrtucture.filter;

public class FilterFactory
{
    private List<object> Collection { get; }

    public AppContainer AppContainer { get; }

    public FilterFactory(AppContainer appContainer)
    {
        AppContainer = appContainer;
        Collection = new List<object>();
    }
    
    public TFilter Find<TFilter>() where TFilter : FilterModel
    {
        try
        {
            return (TFilter)Collection.Single(f => f.GetType().FullName == typeof(TFilter).FullName);
        }
        catch
        {
            AddNew<TFilter>();
            return Find<TFilter>();
        }
    }

    public void Replace(FilterModel filter)
    {
        if (Collection.SingleOrDefault(f => f.GetType().FullName == filter.GetType().FullName) is var old)
            Collection.Remove(old);
        Collection.Add(filter);
    }
    
    protected void AddNew<TFilter>() where TFilter : FilterModel
    {
        var filter = AppContainer.GetInstance<TFilter>();
        filter.Reset();
        Collection.Add(filter);
    }
}