using System.Text.Json;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.logger.model;
using PISWF.infrasrtucture.logger.service;
using PISWF.infrasrtucture.page;

namespace PISWF.infrasrtucture.logger.controller;

public class LogController
{
    private LogService LogService { get; set; }
    
    private AuthController AuthController { get; set; }

    public LogController(LogService logService,  AuthController authController)
    {
        LogService = logService;
        AuthController = authController;
    }
    
    public List<Log> Read(Page page)
    {
        return LogService.Read(page);
    }
    
    public Log AddRecord<T>(string methodName, T entity)
    {
        var log = new Log()
        {
            MethodName = methodName, 
            JsonEntity = JsonSerializer.Serialize(entity), 
            Author = AuthController.AutorizedUser,
            LogDate = DateTime.Now
        };
        return LogService.AddRecord(log);
    }
}