using Microsoft.EntityFrameworkCore;
using PISWF.infrasrtucture.muni_org.model.entity;

namespace PISWF;

public partial class AppDbContext
{
    public DbSet<Municipality> Municipalities { get; set; }
}