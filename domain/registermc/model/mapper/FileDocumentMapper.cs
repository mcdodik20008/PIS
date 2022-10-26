using AutoMapper;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.view;

namespace PISWF.domain.registermc.model.mapper;

public class FileDocumentMapper : Mapper
{
    public FileDocumentMapper() : base(new MapperConfiguration(
        cfg =>
        {
            cfg.CreateMap<FileDocument, FileDocumentShort>();
            cfg.CreateMap<FileDocumentShort, FileDocument>();
            cfg.CreateMap<FileDocument, FileDocumentLong>();
            cfg.CreateMap<FileDocumentLong, FileDocument>();
        })
    )
    {
    }

    public FileDocumentMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
    {
    }
}