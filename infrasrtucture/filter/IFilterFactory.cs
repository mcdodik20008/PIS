namespace PISWF.infrasrtucture.filter;

public interface IFilterFactory
{
    /// <summary>
    /// Поиск фильтра по типу
    /// </summary>
    /// <typeparam name="TFilter"></typeparam>
    /// <returns></returns>
    TFilter Find<TFilter>() where TFilter : FilterModel;

    /// <summary>
    /// Замена объекта фильтра
    /// </summary>
    /// <param name="filter"></param>
    void Replace(FilterModel filter);
}