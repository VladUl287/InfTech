using InfTech.Core.Repositories;
using InfTech.Core.Dtos;
using InfTech.Infrastracture.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InfTech.Infrastracture.Repositories;

public sealed class FilePresenter(DatabaseContext dbContext) : IFilePresenter
{
    private readonly DatabaseContext dbContext = dbContext;

    public Task<FileDto[]> Get(int? folderId)
    {
        return dbContext.Files
            .Where(f => f.FolderId == folderId)
            .OrderBy(f => f.Name)
            .ProjectToType<FileDto>()
            .ToArrayAsync();
    }

    public Task<FileDto?> GetFile(int fileId)
    {
        return dbContext.Files
            .Where(f => f.Id == fileId)
            .ProjectToType<FileDto>()
            .FirstOrDefaultAsync();
    }
}
