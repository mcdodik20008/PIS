using PISWF.domain.registermc.model.entity;

namespace PISWF.domain.registermc.context.repository;

public class FileDocumentRepository : AppRepository<FileDocument>
{
    public FileDocumentRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}