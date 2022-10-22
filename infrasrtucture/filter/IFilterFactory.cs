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
    /// Поиск фильтра по GUID типа
    /// </summary>
    /// <param name="guidString"></param>
    /// <returns></returns>
    FilterModel Find(String guidString);

    /// <summary>
    /// Замена объекта фильтра
    /// </summary>
    /// <param name="filter"></param>
    void Replace(FilterModel filter);

    /// <summary>
    /// Сброс данных фильтра на начальную позицию
    /// </summary>
    /// <param name="guidString">guid типа фильтра</param>
    void Reset(String guidString);
}