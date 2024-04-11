using InfTech.Core.Entities;

namespace InfTech.Core.Repositories;

public interface IFileManager
{
    Task Create(FileEntity file);

    Task Rename(int fileId, string name);

    Task Delete(int fileId);
}
