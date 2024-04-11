using InfTech.Core.Entities;

namespace InfTech.Core.Repositories;

public interface IFolderManager
{
    Task Create(Folder folder);

    Task Rename(int folderId, string name);

    Task Delete(int folderId);
}
