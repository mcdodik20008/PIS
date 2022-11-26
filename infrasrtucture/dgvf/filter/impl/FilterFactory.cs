using LightInject;

namespace PISWF.infrasrtucture.filter;

public class FilterFactory : IFilterFactory
{
    private List<FilterModel> Collection { get; }

    private AppContainer AppContainer { get; }

    public FilterFactory(AppContainer appContainer)
    {
        AppContainer = appContainer;
        Collection = new List<FilterModel>();
    }
    
    public TFilter Find<TFilter>() where TFilter : FilterModel
    {
        if (Collection.FirstOrDefault(f => f is TFilter) is TFilter filter)
        {
            return filter;
        }
        AddNew<TFilter>();
        return Find<TFilter>();
    }

    private void AddNew<TFilter>() where TFilter : FilterModel
    {
        var filter = AppContainer.GetInstance<TFilter>();
        filter.Reset();
        Collection.Add(filter);
    }
}