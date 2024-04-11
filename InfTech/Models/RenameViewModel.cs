using System.ComponentModel.DataAnnotations;

namespace InfTech.Models;

public sealed class RenameViewModel
{
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    public int Id { get; init; }

    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [MaxLength(255, ErrorMessage = "Слишком длинное наименование")]
    public string Name { get; init; } = string.Empty;
}
