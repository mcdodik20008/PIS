using PISWF.infrasrtucture.muni_org.model.entity;
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
    
    public Municipality GetById(long id)
    {
        using var context = new AppDbContext();
        return context.Municipalities.Find(id);
    }
    
    public MunicipalityShort Add(MunicipalityShort municipalityShort)
    {
        using var context = new AppDbContext();
        var municipality = Mapper.Map<Municipality>(municipalityShort);
        context.Municipalities.Add(municipality);
        context.SaveChanges();
        return municipalityShort;
    }
    
    public MunicipalityShort Update(MunicipalityShort municipalityShort)
    {
        using var context = new AppDbContext();
        var municipality = Mapper.Map<Municipality>(municipalityShort);
        context.Municipalities.Add(municipality);
        context.SaveChanges();
        return municipalityShort;
    }
    
    public MunicipalityShort Delete(MunicipalityShort municipalityShort)
    {
        using var context = new AppDbContext();
        var municipality = Mapper.Map<Municipality>(municipalityShort);
        context.Municipalities.Remove(municipality);
        return municipalityShort;
    }
}