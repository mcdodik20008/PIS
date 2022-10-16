using PISWF.infrasrtucture.auth.model.entity;

namespace PISWF.infrasrtucture.auth.context.repository;

public class UserRepository : AppRepository<User>
{
    public UserRepository(AppDbContext appDbContext) : base(appDbContext) { }
}