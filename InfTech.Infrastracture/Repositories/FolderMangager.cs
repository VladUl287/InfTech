using InfTech.Core.Repositories;
using InfTech.Core.Entities;
using InfTech.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace InfTech.Infrastracture.Repositories;

public sealed class FolderMangager(DatabaseContext databaseContext) : IFolderManager
{
    private readonly DatabaseContext databaseContext = databaseContext;

    public async Task Create(Folder folder)
    {
        await databaseContext.Folders.AddAsync(folder);
        await databaseContext.SaveChangesAsync();
    }

    public async Task Delete(int folderId)
    {
        await databaseContext.Folders
            .Where(f => f.Id == folderId)
            .ExecuteDeleteAsync();
    }

    public async Task Rename(int folderId, string name)
    {
        await databaseContext.Folders
            .Where(f => f.Id == folderId)
            .ExecuteUpdateAsync(setter =>
                setter.SetProperty(p => p.Name, v => name));
    }
}
