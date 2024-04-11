using InfTech.Core.Dtos;
using InfTech.Core.Entities;

namespace InfTech.Core.Repositories;

public interface IFilePresenter
{
    Task<FileDto[]> Get(int? folderId);

    Task<FileDto?> GetFile(int fileId);
}
