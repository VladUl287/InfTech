using InfTech.Core.Dtos;

namespace InfTech.Core.Repositories;

public interface IFolderPresenter
{
    Task<FolderDto[]> Get(int? folderId);
}
