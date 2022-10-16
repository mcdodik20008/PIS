using PISWF.domain.registermc.model.entity;

namespace PISWF.domain.registermc.context.repository;

public class RegistermcRepository : AppRepository<RegisterMC>
{
    public RegistermcRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}