using LightInject;
using PISWF.infrasrtucture.page;
using PISWF.view;

namespace PISWF;

public static class Program
{
    // Добавить миграцию - dotnet ef migrations add (название)
    // Залить на базу - dotnet ef database update
    // НАстройка контейнера
    // docker run --name pis -p 5432:5432 -e POSTGRES_PASSWORD=1234 -d postgres
    
    [STAThread]
    public static void Main(string[] args)
    {
        var valuesCount = 100;
        var rnd = new Random();
        var registermcs = new string[100, 10];
        var date = Enumerable.Range(0, valuesCount).Select(x => 
            Trash.RndDate(DateTime.Now.AddYears(-2), DateTime.Now).ToString().Substring(0, 10).Split('.')).ToArray();
        var omsys = new[] { "МойкаПомойка", "Самый Главный ОМСУ", "Важный, но не главный ОМСУ", "ОМСУ поселковый"};
        var omsyList = Enumerable.Range(0, valuesCount).Select(x => omsys[rnd.Next(0, 4)]).ToList();
        var locations = new[] { "Улица", "Дом", "Квартира", "Команта" };
        var locationsList = Enumerable.Range(0, valuesCount).Select(x => locations[rnd.Next(0, 4)]).ToList();
        for (int i = 0; i < registermcs.GetLength(0); i++)
        {
            var x =
                $"UPDATE \"register-m-c\" SET (\"location\", action_date, omsu) = ('{locationsList[i]}', date '{date[i]}', '{omsyList[i]}')" +
                $"WHERE id = {i + 1}";

        }
        // var container = new AppContainer();
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(container.GetInstance<Auth>());
    }
    
}