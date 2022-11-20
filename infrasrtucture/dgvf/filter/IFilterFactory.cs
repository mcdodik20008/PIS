namespace PISWF.infrasrtucture.filter;

public interface IFilterFactory
{
    T Find<T>() where T : FilterModel;
}