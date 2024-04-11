using InfTech.Core.Entities;
using InfTech.Infrastracture.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace InfTech.Infrastracture.Database;

public sealed class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    public DbSet<Folder> Folders => Set<Folder>();
    public DbSet<FileEntity> Files => Set<FileEntity>();
    public DbSet<Extension> Extensions => Set<Extension>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FolderConfig());
        modelBuilder.ApplyConfiguration(new FileConfig());
        modelBuilder.ApplyConfiguration(new ExtensionConfig());

        modelBuilder.Entity<Folder>(e =>
        {
            e.HasData([
                new Folder
                {
                    Id = 1,
                    Name = "Resources",
                },
                new Folder
                {
                    Id = 2,
                    Name = "Content",
                    ParentFolderId = 1
                },
                new Folder
                {
                    Id = 3,
                    Name = "bin",
                },
                new Folder
                {
                    Id = 4,
                    Name = "Debug",
                    ParentFolderId = 3
                },
            ]);
        });

        modelBuilder.Entity<Extension>(e =>
        {
            e.HasData([
                new Extension
                {
                    Id = 1,
                    Type = string.Empty,
                    Icon = "<i class=\"bi bi-file-earmark\"></i>"
                },                    
                new Extension
                {
                    Id = 2,
                    Type = ".html",
                    Icon = "<i class=\"bi bi-filetype-html\"></i>"
                },                
                new Extension
                {
                    Id = 3,
                    Type = ".xaml",
                    Icon = "<i class=\"bi bi-filetype-xml\"></i>"
                },                
                new Extension
                {
                    Id = 4,
                    Type = ".cs",
                    Icon = "<i class=\"bi bi-filetype-cs\"></i>"
                },
            ]);
        });

        modelBuilder.Entity<FileEntity>(e =>
        {
            e.HasData([
                new FileEntity
                {
                    Id = 1,
                    Name = "MainWindow",
                    FolderId = 1,
                    Content = "<html>\n" +
                                "<body>\n" +
                                "   test\n" +
                                "</body\n" +
                                "</html>\n",
                    ExtensionId = 2,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s"
                },
                new FileEntity
                {
                    Id = 2,
                    Name = "App",
                    Content = "<StackPanel>\r\n    <Button Content=\"Click Me\"/>\r\n</StackPanel>",
                    ExtensionId = 4,
                    Description = "It has roots in a piece of classical Latin literature from 45"
                }
            ]);
        });

        base.OnModelCreating(modelBuilder);
    }
}
