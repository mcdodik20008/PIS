using System.Reflection;
using OfficeOpenXml;

namespace PISWF.infrasrtucture;

public class ExcelExporter
{
    public byte[] Generate<T>(List<T> data)
    {
        var package = new ExcelPackage();
        var type = typeof(T);
        var props = type.GetProperties()
            .Where(x => 
                x.GetCustomAttribute(typeof(ToExcelAttribute)) is not null
            ).ToArray();
        var sheet = package.Workbook.Worksheets.Add($"{type.Name} Report");
        sheet.Cells[1, 1, 1, props.Length]
            .LoadFromArrays(new object[][] { props.Select(x => x.Name).ToArray() });
        var row = 2;
        foreach (var item in data)
        {
            for (int i = 0; i < props.Length; i++)
            {
                sheet.Cells[row, i + 1].Value = props[i].GetValue(item);
            }
            row++;
        }
        return package.GetAsByteArray();
    }
}