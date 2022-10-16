using PISWF.infrasrtucture.logger.model;

namespace PISWF.infrasrtucture.logger.context;

public class LogRepository : AppRepository<Log>
{
    public LogRepository(AppDbContext appDbContext) : base(appDbContext) { }
}