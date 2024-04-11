using InfTech.Core.Repositories;
using InfTech.Core.Entities;
using InfTech.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace InfTech.Infrastracture.Repositories;

public sealed class FileManager(DatabaseContext databaseContext) : IFileManager
{
    private readonly DatabaseContext dbContext = databaseContext;

    public async Task Create(FileEntity file)
    {
        await dbContext.AddAsync(file);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(int fileId)
    {
        await databaseContext.Files
            .Where(f => f.Id == fileId)
            .ExecuteDeleteAsync();
    }

    public async Task Rename(int fileId, string name)
    {
        await databaseContext.Files
            .Where(f => f.Id == fileId)
            .ExecuteUpdateAsync(setter =>
                setter.SetProperty(p => p.Name, v => name));
    }
}
