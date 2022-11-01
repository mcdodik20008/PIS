using PISWF.domain.registermc.model.entity;

namespace PISWF.domain.registermc.context.repository;

public class RegisterMcRepository : AppRepository<RegisterMC>
{
    public RegisterMcRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}