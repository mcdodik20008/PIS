namespace PISWF.infrasrtucture.page;

public static class Trash
{
    public static DateTime RndDate(DateTime startDate, DateTime endDate)
    {
        var rnd = new Random();
        var randomYear = rnd.Next(startDate.Year, endDate.Year);
        var randomMonth = rnd.Next(1, 12);
        var randomDay = rnd.Next(1, DateTime.DaysInMonth(randomYear, randomMonth));

        if (randomYear == startDate.Year)
        {
            randomMonth = rnd.Next(startDate.Month, 12);

            if (randomMonth == startDate.Month)
                randomDay = rnd.Next(startDate.Day, DateTime.DaysInMonth(randomYear, randomMonth));
        }

        if (randomYear == endDate.Year)
        {
            randomMonth = rnd.Next(1, endDate.Month);

            if (randomMonth == endDate.Month)
                randomDay = rnd.Next(1, endDate.Day);
        }

        var randomDate = new DateTime(randomYear, randomMonth, randomDay);


        return new DateTime(randomDate.Year, randomDate.Month, randomDate.Day);
    }
}