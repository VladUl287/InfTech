using InfTech.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfTech.Infrastracture.Database.Configuration;

internal sealed class ExtensionConfig : IEntityTypeConfiguration<Extension>
{
    public void Configure(EntityTypeBuilder<Extension> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Type)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(e => e.Icon)
            .IsRequired();

        builder.HasIndex(e => e.Type)
            .IsUnique();
    }
}
