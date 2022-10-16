using PISWF.domain.registermc.context.repository;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.mapper;
using PISWF.domain.registermc.model.view;

namespace PISWF.domain.registermc.service;

public class RegistermcService
{
    private FileDocumentMapper FileDocumentMapper { get; }
    private FileDocumentRepository FileDocumentRepository { get; }
    private RegisterMcMapper RegisterMcMapper { get; }
    private RegistermcRepository RegistermcRepository { get; }

    public RegistermcService(
        FileDocumentMapper fileDocumentMapper,
        FileDocumentRepository fileDocumentRepository,
        RegisterMcMapper registerMcMapper,
        RegistermcRepository registermcRepository)
    {
        FileDocumentMapper = fileDocumentMapper;
        FileDocumentRepository = fileDocumentRepository;
        RegisterMcMapper = registerMcMapper;
        RegistermcRepository = registermcRepository;
    }

    public List<RegisterMCShort> Read()
    {
        return RegisterMcMapper.Map<List<RegisterMCShort>>(RegistermcRepository.Entity);
    }
    
    public List<RegisterMCShort> Read(Func<RegisterMC, bool> filter)
    {
        return RegisterMcMapper
            .Map<List<RegisterMCShort>>(
                RegistermcRepository
                    .Entity.Where(x => filter(x))
                    .ToList()
            );
    }
    
    public RegisterMCLong Read(long id)
    {
        return RegisterMcMapper.Map<RegisterMCLong>(
            RegistermcRepository.Entity.Find(id)
        );
    }

    public RegisterMCShort Create(RegisterMCShort view)
    {
        var entity = RegisterMcMapper.Map<RegisterMC>(view);
        RegistermcRepository.UpdateAndSave(entity);
        return view;
    }

    public RegisterMCLong Update(long id, RegisterMCLong view)
    {
        view.Id = id;
        var register = RegistermcRepository.Entity.Find(id);
        register = RegisterMcMapper.Map(view, register);
        RegistermcRepository.UpdateAndSave(register);
        return view;
    }

    public RegisterMCShort Delete(RegisterMCShort view)
    {
        var entity = RegisterMcMapper.Map<RegisterMC>(view);
        RegistermcRepository.Entity.Remove(entity);
        return view;
    }
}