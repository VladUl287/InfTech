using Microsoft.AspNetCore.Mvc;
using InfTech.Models;
using InfTech.Core.Entities;
using Mapster;
using System.ComponentModel.DataAnnotations;
using InfTech.Core.Repositories;

namespace InfTech.Controllers;

public sealed class FolderController : Controller
{
    private readonly IFolderPresenter folderPresenter;
    private readonly IFolderManager folderManager;

    public FolderController(IFolderPresenter folderPresenter, IFolderManager folderManager)
    {
        this.folderPresenter = folderPresenter;
        this.folderManager = folderManager;
    }

    [HttpGet]
    public async Task<PartialViewResult> Get(int? folderId)
    {
        var folders = await folderPresenter.Get(folderId) ?? [];
        return PartialView("_Folders", folders);
    }

    [HttpGet]
    public PartialViewResult Create(int? folderId)
    {
        return PartialView("_Create", new CreateFolderViewModel
        {
            ParentFolderId = folderId
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateFolderViewModel createFolder)
    {
        if (!ModelState.IsValid)
        {
            Response.StatusCode = 400;
            return PartialView("_Create", createFolder);
        }
        var folder = createFolder.Adapt<Folder>();
        await folderManager.Create(folder);
        return Created(string.Empty, null);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([Required] int folderId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        await folderManager.Delete(folderId);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> Rename(RenameViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        await folderManager.Rename(viewModel.Id, viewModel.Name);
        return NoContent();
    }
}
