using PISWF.infrasrtucture.muni_org.model.entity;

namespace PISWF.infrasrtucture.muni_org.context.repository;

public class MunicipalityRepository : AppRepository<Municipality>
{
    public MunicipalityRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}