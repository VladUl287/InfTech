using InfTech.Core.Dtos;

namespace InfTech.Models;

public sealed class ContentViewModel
{
    public FolderDto[] Folders { get; init; } = [];
    public FileDto[] Files { get; init; } = [];
}
