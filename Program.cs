using LightInject;
using PISWF.infrasrtucture.guard;
using PISWF.infrasrtucture.logger.controller;
using PISWF.view;

namespace PISWF;

public static class Program
{
    // Добавить миграцию - dotnet ef migrations add (название)
    // Залить на базу - dotnet ef database update
    // НАстройка контейнера
    // docker run --name pis -p 5432:5432 -e POSTGRES_PASSWORD=1234 -d postgres
    // Чтоб не писать кучу тракачт можно попытаться ошибки складывать в какое-то место и потом отображать их пользователю
    public static void Main(string[] args)
    {
        var container = new AppContainer();
        var guard = container.GetInstance<Guard>();
        string? bull = null;
        guard.ThtowIfNull(bull);
        var logController = container.GetInstance<LogController>();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(container.GetInstance<DGVFilter>());
        /*var cont = container.GetInstance<AuthController>();
        cont.AddUser(new UserBasic("guest", "1234"));
        Console.Write(container.GetInstance<AppDbContext>());*/
    }
}