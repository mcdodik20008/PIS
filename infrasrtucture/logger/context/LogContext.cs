using Microsoft.EntityFrameworkCore;
using PISWF.infrasrtucture.logger.model;

namespace PISWF;

public partial class AppDbContext
{
    public DbSet<Log>? Logs { get; set; }
}