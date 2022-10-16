using System.Diagnostics;
using PISWF.infrasrtucture.auth.controller;

namespace PISWF.infrasrtucture.logger.service;

public class LogServiceAsync
{
    private AuthController _authController { get; set; }

    private LogService _logService { get; set; }

    public LogServiceAsync(LogService logService, AuthController authController)
    {
        _authController = authController;
        _logService = logService;
    }

    public async Task ObserverStackTraceAsync(StackTrace stackTrace)
    {
        Console.WriteLine("Начало метода"); // выполняется синхронно
        await Task.Run(() => ObserverStackTrace(stackTrace));                // выполняется асинхронно
        Console.WriteLine("Конец метода");
    }   
    
    public void ObserverStackTrace(StackTrace stack)
    {
        var lastStackLen = 0;
        while (true)
        {
            var frames = stack.GetFrames();
            if (frames.Length != 1)
                Console.WriteLine(frames.Length);
            /*
            if (lastStackLen != frames.Length)
            {
              //  Console.WriteLine(lastStackLen);
                lastStackLen = frames.Length;
                if (frames
                        .Select(x => x.GetMethod())
                        .Where(x => x.GetMethod().GetCustomAttributes(typeof(MethodName), true) != null)?
                        .FirstOrDefault() is MethodName atribute)
                {
                    var log = _logService.AddRecord
                    (
                        new Log()
                        {
                            MethodName = atribute.Name,
                            Author = _authController.AutorizedUser,
                            LogDate = DateTime.Now
                        }
                    );
                    _logService.AddRecord(log);
                }
            }*/
        }
    }
}