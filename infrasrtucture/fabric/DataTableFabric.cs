using System.Data;
using System.Reflection;

namespace PISWF.infrasrtucture;

public class SourceFabric
{
    public DataTable GetDataTable<T>(IEnumerable<T> sourse)
    {
        var dt = new DataTable();
        var propertys = typeof(T).GetProperties();
        foreach (var prop in propertys)
            dt.Columns.Add(prop.Name, prop.PropertyType);
        foreach (var entity in sourse)
        {
            var values = GetEntityValues(entity, propertys);
            dt.Rows.Add(values.ToArray());
        }
        return dt;
     /*  var DT = new DataTable();
       DT.Columns.Add("Number", typeof(int));
       DT.Columns.Add("Name");
       DT.Columns.Add("Ver");
       DT.Columns.Add("Date", typeof(DateTime));
       DT.Rows.Add("1", "Ubuntu", "11.10", "13.10.2011");
       DT.Rows.Add("2", "Ubuntu LTS", "12.04", "18.10.2012");
       DT.Rows.Add("3", "Ubuntu", "12.10", "18.10.2012");
       DT.Rows.Add("4", "Ubuntu", "13.04", "25.04.2012");
       DT.Rows.Add("5", "Ubuntu", "13.10", "17.10.2013");
       DT.Rows.Add("6", "Ubuntu LTS", "14.04", "23.04.2014");
       DT.Rows.Add("7", "Ubuntu", "14.10", "23.10.2014");
       DT.Rows.Add("8", "Ubuntu", "15.04", "23.04.2015");
       DT.Rows.Add("9", "Ubuntu", "15.04", "23.04.2015");
       DT.Rows.Add("11", "fbfghf", "11.10", "13.10.2011");
       DT.Rows.Add("12", "fbfghf LTS", "12.04", "18.10.2012");
       DT.Rows.Add("13", "fbfghf", "12.10", "18.10.2012");
       DT.Rows.Add("14", "fbfghf", "13.04", "25.04.2012");
       DT.Rows.Add("15", "fbfghf", "13.10", "17.10.2013");
       DT.Rows.Add("16", "fbfghf LTS", "14.04", "23.04.2014");
       DT.Rows.Add("17", "fbfghf", "14.10", "23.10.2014");
       DT.Rows.Add("18", "fbfghf", "15.04", "23.04.2015");
       DT.Rows.Add("19", "fbfghf", "15.04", "23.04.2015");
       DT.Rows.Add("21", "Ubuntu", "11.10", "13.10.2011");
       DT.Rows.Add("22", "Ubuntu LTS", "12.04", "18.10.2012");
       DT.Rows.Add("23", "Ubuntu", "12.10", "18.10.2012");
       DT.Rows.Add("24", "Ubuntu", "13.04", "25.04.2012");
       DT.Rows.Add("25", "Ubuntu", "13.10", "17.10.2013");
       DT.Rows.Add("26", "Ubuntu LTS", "14.04", "23.04.2014");
       DT.Rows.Add("27", "Ubuntu", "14.10", "23.10.2014");
       DT.Rows.Add("28", "Ubuntu", "15.04", "23.04.2015");
       DT.Rows.Add("29", "Ubuntu", "15.04", "23.04.2015");
       return DT;*/
    }

    private object[] GetEntityValues<T>(T entity, PropertyInfo[] propertys)
    {
        var values = new object[propertys.Length];
        var index = 0;
        foreach (var prop in propertys)
            values[index++] = prop.GetValue(entity);
        return values;
    }
}