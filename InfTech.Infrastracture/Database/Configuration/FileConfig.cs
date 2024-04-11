using InfTech.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfTech.Infrastracture.Database.Configuration;

internal sealed class FileConfig : IEntityTypeConfiguration<FileEntity>
{
    public void Configure(EntityTypeBuilder<FileEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.Description)
            .IsRequired();

        builder.Property(e => e.Content)
            .IsRequired();

        builder.HasOne(e => e.Extension)
            .WithMany(e => e.Files)
            .HasForeignKey(e => e.ExtensionId);

        builder.HasOne(e => e.Folder)
            .WithMany(e => e.Files)
            .HasForeignKey(e => e.FolderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
