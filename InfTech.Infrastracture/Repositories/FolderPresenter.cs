using InfTech.Core.Repositories;
using InfTech.Core.Dtos;
using InfTech.Infrastracture.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InfTech.Infrastracture.Repositories;

public sealed class FolderPresenter(DatabaseContext dbContext) : IFolderPresenter
{
    private readonly DatabaseContext dbContext = dbContext;

    public Task<FolderDto[]> Get(int? folderId)
    {
        return dbContext.Folders
            .Where(f => f.ParentFolderId == folderId)
            .OrderBy(f => f.Name)
            .ProjectToType<FolderDto>()
            .ToArrayAsync();
    }
}
