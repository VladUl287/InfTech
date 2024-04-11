using InfTech.Core.Repositories;
using InfTech.Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace InfTech.Infrastracture.Repositories;

public sealed class ExtensionPresenter(DatabaseContext databaseContext) : IExtensionPresenter
{
    private readonly DatabaseContext databaseContext = databaseContext;

    public Task<int> GetId(string extension)
    {
        return databaseContext.Extensions
            .Where(e => e.Type == extension)
            .Select(e => e.Id)
            .FirstOrDefaultAsync();
    }
}
