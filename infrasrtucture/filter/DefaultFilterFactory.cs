using LightInject;

namespace PISWF.infrasrtucture.filter;

public class DefaultFilterFactory : IFilterFactory
{
    private List<object> Collection { get; }
    
    private AppContainer AppContainer { get; }

    public DefaultFilterFactory(AppContainer appContainer)
    {
        AppContainer = appContainer;
        Collection = new List<object>();
    }

    /// <summary>
    /// Поиск необходимого фильтра
    /// </summary>
    /// <typeparam name="TFilter">тип фильтра</typeparam>
    /// <returns></returns>
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

    /// <summary>
    /// Поиск фильтра по GUID его типа
    /// </summary>
    /// <param name="guidString">строковое представление GUID</param>
    /// <returns></returns>
    public FilterModel Find(String guidString)
    {
        return (FilterModel)Collection.Single(f => f.GetType().GUID.ToString() == guidString);
    }

    public void Replace(FilterModel filter)
    {
        try
        {
            var old = Collection.SingleOrDefault(f => f.GetType().FullName == filter.GetType().FullName);

            if (old != null)
            {
                if (!Collection.Remove(old))
                    throw new InvalidOperationException("Неудача при удалении старого фильтра");
            }
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch
        {
            //сюда можно приделать логгер
        }

        Collection.Add(filter);
    }

    /// <summary>
    /// Добавление нового фильтра
    /// </summary>
    /// <typeparam name="TFilter">тип фильтра</typeparam>
    protected void AddNew<TFilter>() where TFilter : FilterModel
    {
        var filter = AppContainer.GetInstance<TFilter>();

        filter.Reset();

        Collection.Add(filter);
    }

    /// <summary>
    /// Сброс фильтра по GUID его типа
    /// </summary>
    /// <param name="guidString">строковое представление GUID</param>
    /// <returns></returns>
    public void Reset(String guidString)
    {
        try
        {
            var filter = Find(guidString);

            filter.Reset();
        }
        catch
        {
            //сюда можно приделать логгер
        }
    }
}