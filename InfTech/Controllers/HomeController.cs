﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using InfTech.MVC.Models;

namespace InfTech.MVC.Controllers;

public sealed class HomeController : Controller
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
