using Microsoft.EntityFrameworkCore;
using PISWF.infrasrtucture.auth.model.entity;

namespace PISWF;

public partial class AppDbContext
{
    public DbSet<User>? Users { get; set; } 
    
    public DbSet<Role>? Roles { get; set; }
}