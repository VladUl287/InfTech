using InfTech.Core.Dtos;
using System.ComponentModel.DataAnnotations;

namespace InfTech.Models;

public class CreateFolderViewModel
{
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    public string Name { get; init; } = string.Empty;
    
    public int? ParentFolderId { get; init; }

    public FolderDto[] Folders { get; init; } = [];
}