using PISWF.infrasrtucture.muni_org.model.entity;

namespace PISWF.infrasrtucture.muni_org.context.repository;

public class OrganizationRepository : AppRepository<Organization>
{
    public OrganizationRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}