namespace InfTech.Core.Entities;

public sealed class FileEntity
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string Content { get; init; } = string.Empty;

    public int ExtensionId {  get; init; }
    public Extension? Extension { get; init; }

    public int? FolderId { get; init; }
    public Folder? Folder { get; init; }
}
