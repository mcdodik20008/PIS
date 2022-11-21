using PISWF.infrasrtucture.logger.model;
using PISWF.infrasrtucture.page;

namespace PISWF.infrasrtucture.logger.service;

public class LogService
{
    public List<Log> Read(Page page)
    {
        using var context = new AppDbContext();
        return context.Logs.Skip(page.Size * page.Number).Take(page.Size).ToList();
    }
    
    public Log AddRecord(Log log)
    {
        using var context = new AppDbContext();
        log.Author = context.Users.Find(log.Author.Login);
        context.Logs.Add(log);
        context.SaveChanges();
        return log;
    }
}