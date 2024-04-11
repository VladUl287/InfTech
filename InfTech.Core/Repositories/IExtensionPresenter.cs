using InfTech.Core.Dtos;

namespace InfTech.Core.Repositories;

public interface IExtensionPresenter
{
    Task<int> GetId(string extension);
}
