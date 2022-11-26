using PISWF.infrasrtucture.muni_org.model.mapper;
using PISWF.infrasrtucture.muni_org.model.view;

namespace PISWF.infrasrtucture.muni_org.service;

public class MunicipalityService
{
    private MunicipalityMapper Mapper { get; }
    
    public MunicipalityService(MunicipalityMapper municipalityMapper)
    {
        Mapper = municipalityMapper;
    }
    
    public List<MunicipalityShort> GetAll()
    {
        using var context = new AppDbContext();
        return Mapper.Map<List<MunicipalityShort>>(context.Municipalities.ToList());
    }
}