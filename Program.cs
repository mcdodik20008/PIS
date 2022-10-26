using LightInject;
using pis.infrasrtucture.filter.impl;
using PISWF.infrasrtucture.filter;
using PISWF.infrasrtucture.logger.controller;
using PISWF.infrasrtucture.page;
using PISWF.view;

namespace PISWF;

public static class Program
{
    // Добавить миграцию - dotnet ef migrations add (название)
    // Залить на базу - dotnet ef database update
    // НАстройка контейнера
    // docker run --name pis -p 5432:5432 -e POSTGRES_PASSWORD=1234 -d postgres
    
    public static void Main(string[] args)
    {
        var valuesCount = 100;
        var rnd = new Random();
        var registermcs = new string[100, 10];
        var id = Enumerable.Range(1, valuesCount).Select(x => x.ToString()).ToArray();
        var numbers = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(0, 1000).ToString()).ToArray();
        var date = Enumerable.Range(0, valuesCount).Select(x => Trash.RndDate(DateTime.Now.AddYears(-2), DateTime.Now).ToString()).ToArray();
        var organizations = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(0, 4).ToString()).ToArray();
        var municipality = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(0, 4).ToString()).ToArray();
        var year = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(2010, 2022).ToString()).ToArray();
        var price = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(100000, 5000000).ToString()).ToArray();
        var subvention_share = Enumerable.Range(0, valuesCount).Select(x => new Random(x).Next(100000, 5000000).ToString()).ToArray();
        var amount_money = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(100000, 5000000).ToString()).ToArray();
        var share_funds_subvention = Enumerable.Range(0, valuesCount).Select(x => rnd.Next(100000, 5000000).ToString()).ToArray();
        for (int i = 0; i < registermcs.GetLength(0); i++)
        {
            registermcs[i, 0] = id[i];
            registermcs[i, 1] = numbers[i];
            registermcs[i, 2] = date[i];
            registermcs[i, 3] = organizations[i];
            registermcs[i, 4] = municipality[i];
            registermcs[i, 5] = year[i];
            registermcs[i, 6] = price[i];
            registermcs[i, 7] = subvention_share[i];
            registermcs[i, 8] = amount_money[i];
            registermcs[i, 9] = share_funds_subvention[i];
        }
        // var container = new AppContainer();
        //   Application.EnableVisualStyles();
        //   Application.SetCompatibleTextRenderingDefault(false);
        //   Application.Run(container.GetInstance<DgvFilter>());
    }
}