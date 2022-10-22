using System.Text;

namespace pis.infrasrtucture.dgvf;

public static class FiltersExtention
{
    public static string AsString(this List<FilterColumn> filters)
    {
        var builder = new StringBuilder();
        foreach (var filter in filters.Where(x => !(x == null || x.Value.Equals(""))))
        {
            builder.Append(filter.ToString());
            builder.Append(" AND ");
        }
        if (builder.Length > 4)
            builder.Remove(builder.Length - 5, 4);
        return builder.ToString();
    }
}