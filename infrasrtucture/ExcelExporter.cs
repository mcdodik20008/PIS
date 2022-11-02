using OfficeOpenXml;

namespace PISWF.infrasrtucture;

public class ExcelExporter
{
    public byte[] Generate<T>(List<T> data)
    {
        var package = new ExcelPackage();
        var type = typeof(T);
        var sheet = package.Workbook.Worksheets.Add($"{type.Name} Report");
        return package.GetAsByteArray();
    }
}