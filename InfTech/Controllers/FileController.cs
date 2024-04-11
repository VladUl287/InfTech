using Microsoft.AspNetCore.Mvc;
using InfTech.Models;
using System.Text;
using Microsoft.AspNetCore.StaticFiles;
using System.Net.Mime;
using InfTech.Core.Entities;
using System.ComponentModel.DataAnnotations;
using InfTech.Core.Repositories;

namespace InfTech.Controllers;

public sealed class FileController : Controller
{
    private readonly IFilePresenter filePresenter;
    private readonly IFileManager fileManager;
    private readonly IExtensionPresenter extensionPresenter;

    private static readonly FileExtensionContentTypeProvider contentTypeProvider = new();

    public FileController(IFilePresenter filePresenter, IFileManager fileManager, IExtensionPresenter extensionPresenter)
    {
        this.filePresenter = filePresenter;
        this.fileManager = fileManager;
        this.extensionPresenter = extensionPresenter;
    }

    [HttpGet]
    public async Task<PartialViewResult> Get(int? folderId)
    {
        var files = await filePresenter.Get(folderId) ?? [];
        return PartialView("_Files", files);
    }

    [HttpGet]
    public async Task<IActionResult> Download(int fileId)
    {
        var file = await filePresenter.GetFile(fileId);
        if (file is null)
        {
            return NotFound();
        }
        var fileName = $"{file.Name}{file.Extension.Type}";
        if (!contentTypeProvider.TryGetContentType(fileName, out string? contentType))
        {
            contentType = MediaTypeNames.Application.Octet;
        }
        var bytes = new UTF8Encoding(true).GetBytes(file.Content);
        return File(bytes, contentType, fileName);
    }

    [HttpGet]
    public PartialViewResult Create(int? folderId)
    {
        return PartialView("_Create", new CreateFileViewModel
        {
            FolderId = folderId
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] CreateFileViewModel fileModel)
    {
        if (!ModelState.IsValid)
        {
            Response.StatusCode = 400;
            return PartialView("_Create", fileModel);
        }

        var extension = Path.GetExtension(fileModel.Name) ?? string.Empty;
        var extensionId = await extensionPresenter.GetId(extension);
        var file = new FileEntity
        {
            Name = Path.GetFileNameWithoutExtension(fileModel.Name),
            ExtensionId = extensionId,
            FolderId = fileModel.FolderId,
            Content = fileModel.Content,
            Description = fileModel.Description,
        };
        await fileManager.Create(file);
        return Created(string.Empty, null);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([Required] int fileId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        await fileManager.Delete(fileId);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> Rename(RenameViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        await fileManager.Rename(viewModel.Id, viewModel.Name);
        return NoContent();
    }
}
