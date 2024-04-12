namespace InfTech.Core.Dtos;

public sealed class FileDto
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Content { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public ExtensionDto Extension { get; init; } = default!;

    public int? FolderId { get; init; }
}
