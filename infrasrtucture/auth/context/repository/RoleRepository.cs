using PISWF.infrasrtucture.auth.model.entity;

namespace PISWF.infrasrtucture.auth.context.repository;

public class RoleRepository : AppRepository<Role>
{
    public RoleRepository(AppDbContext appDbContext) : base(appDbContext) { }
}