namespace PISWF.infrasrtucture.page;

public static class Trash
{
    public static DateTime RndDate(DateTime startDate, DateTime endDate)
    {
        var rnd = new Random();
        var randomYear = rnd.Next(startDate.Year, endDate.Year + 1);
        var randomMonth = rnd.Next(1, 12);
        var randomDay = rnd.Next(1, 28);
        return new DateTime(randomYear, randomMonth, randomDay);
    }
}