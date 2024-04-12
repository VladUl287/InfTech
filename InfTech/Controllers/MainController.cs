using InfTech.Models;
using Microsoft.AspNetCore.Mvc;
using InfTech.Core.Repositories;

namespace InfTech.Controllers;

public sealed class MainController : Controller
{
    private readonly IFolderPresenter folderPresenter;
    private readonly IFilePresenter filePresenter;

    public MainController(IFolderPresenter folderPresenter, IFilePresenter filePresenter)
    {
        this.folderPresenter = folderPresenter;
        this.filePresenter = filePresenter;
    }

    [HttpGet]
    public async Task<ViewResult> Index()
    {
        var folders = await folderPresenter.Get(null);
        var files = await filePresenter.Get(null);

        return View(new ContentViewModel
        {
            Folders = folders ?? [],
            Files = files ?? []
        });
    }

    [HttpGet]
    [ResponseCache(Duration = 60)]
    public PartialViewResult Tab(TabViewModel model)
    {
        return PartialView("_Tab", model);
    }

    [HttpGet]
    [ResponseCache(Duration = 60)]
    public async Task<PartialViewResult> TabContent(int fileId)
    {
        var file = await filePresenter.GetFile(fileId);
        return PartialView("_TabContent", file);
    }
}
