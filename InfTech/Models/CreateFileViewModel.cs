using System.ComponentModel.DataAnnotations;

namespace InfTech.Models;

public sealed class CreateFileViewModel
{
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [MaxLength(255, ErrorMessage = "Слишком длинное наименование")]
    public string Name { get; init; } = string.Empty;

    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    public string Description { get; init; } = string.Empty;

    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    public string Content { get; init; } = string.Empty;

    public int? FolderId { get; init; }
}
