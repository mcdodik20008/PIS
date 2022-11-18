using PISWF.infrasrtucture.muni_org.context.repository;
using PISWF.infrasrtucture.muni_org.model.entity;
using PISWF.infrasrtucture.muni_org.model.mapper;
using PISWF.infrasrtucture.muni_org.model.view;

namespace PISWF.infrasrtucture.muni_org.service;

public class MunicipalityService
{
    private MunicipalityRepository Repository { get; }

    private MunicipalityMapper Mapper { get; }
    
    public MunicipalityService(MunicipalityRepository municipalityRepository, MunicipalityMapper municipalityMapper)
    {
        Repository = municipalityRepository;
        Mapper = municipalityMapper;
    }
    
    public List<MunicipalityShort> GetAll()
    {
        return Mapper.Map<List<MunicipalityShort>>(Repository.Entity.ToList());
    }
    
    public MunicipalityShort GetById(long id)
    {
        return Mapper.Map<MunicipalityShort>(Repository.Entity.Find(id));
    }
    
    public MunicipalityShort Add(MunicipalityShort municipalityShort)
    {
        var municipality = Mapper.Map<Municipality>(municipalityShort);
        Repository.Entity.Add(municipality);
        Repository.Save();
        return municipalityShort;
    }
    
    public MunicipalityShort Update(MunicipalityShort municipalityShort)
    {
        var municipality = Mapper.Map<Municipality>(municipalityShort);
        Repository.Entity.Add(municipality);
        Repository.Save();
        return municipalityShort;
    }
    
    public MunicipalityShort Delete(MunicipalityShort municipalityShort)
    {
        var municipality = Mapper.Map<Municipality>(municipalityShort);
        Repository.Entity.Remove(municipality);
        return municipalityShort;
    }
}