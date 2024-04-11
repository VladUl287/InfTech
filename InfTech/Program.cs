using InfTech.Core.Repositories;
using InfTech.Infrastracture;
using InfTech.Infrastracture.Database;
using InfTech.Infrastracture.Repositories;
using InfTech.MVC.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();

    builder.Services.AddDatabase<DatabaseContext, IInfrastractureMarker>(builder.Configuration);

    builder.Services.AddScoped<IExtensionPresenter, ExtensionPresenter>();
    builder.Services.AddScoped<IFolderPresenter, FolderPresenter>();
    builder.Services.AddScoped<IFolderManager, FolderMangager>();
    builder.Services.AddScoped<IFileManager, FileManager>();
    builder.Services.AddScoped<IFilePresenter, FilePresenter>();
}

var app = builder.Build();
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Main}/{action=Index}/");
}
app.Run();
