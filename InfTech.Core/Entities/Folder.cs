namespace InfTech.Core.Entities;

public sealed class Folder
{
    public int Id { get; init; }

    public string Name { get; set; } = string.Empty;

    public int? ParentFolderId { get; init; }
    public Folder? ParentFolder { get; init; }

    public IEnumerable<FileEntity>? Files { get; init; }

    public IEnumerable<Folder>? Folders { get; init; }
}
