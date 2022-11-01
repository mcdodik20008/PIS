using Microsoft.EntityFrameworkCore;
using PISWF.domain.registermc.model.entity;

namespace PISWF;

public partial class AppDbContext
{
    public DbSet<RegisterMC> Register { get; set; }

    public DbSet<FileDocument> Documents { get; set; }
}