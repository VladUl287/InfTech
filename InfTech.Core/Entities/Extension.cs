namespace InfTech.Core.Entities;

public sealed class Extension
{
    public int Id { get; init; }

    public string Type { get; init; } = string.Empty;

    public string Icon { get; init; } = string.Empty;

    public IEnumerable<FileEntity>? Files { get; init; }
}
