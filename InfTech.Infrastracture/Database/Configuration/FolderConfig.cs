using InfTech.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InfTech.Infrastracture.Database.Configuration;

internal sealed class FolderConfig : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne(e => e.ParentFolder)
            .WithMany(e => e.Folders)
            .HasForeignKey(e => e.ParentFolderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
