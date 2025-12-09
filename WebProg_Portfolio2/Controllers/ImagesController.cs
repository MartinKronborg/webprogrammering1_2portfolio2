using Microsoft.AspNetCore.Mvc;
using WebProg_Portfolio2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebProg_Portfolio2.Controllers;

public class ImagesController : Controller
{
    private int? CurrentUserId() => HttpContext.Session.GetInt32("UserId");

    private readonly AppDbContext _database;

    public ImagesController(AppDbContext database)
    {
        _database = database;
    }

    [HttpGet]
    public IActionResult Upload()
    {
        if (CurrentUserId() == null) return RedirectToAction("Login", "Account");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(IFormFile imageFile, string title, string description)
    {
        if (CurrentUserId() == null) return RedirectToAction("Login", "Account");
        if (imageFile == null || imageFile.Length == 0)
        {
            ModelState.AddModelError("", "Vælg en fil");
            return View();
        }

        //Extension check
        var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var ext = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
        if (!allowed.Contains(ext))
        {
            ModelState.AddModelError("", "Ugyldig filtype");
            return View();
        }

        //Saves the file
        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(uploads);
        var fileName = Guid.NewGuid().ToString() + ext;
        var filePath = Path.Combine(uploads, fileName);
        using (var stream = System.IO.File.Create(filePath))
        {
            await imageFile.CopyToAsync(stream);
        }
        
        //Creates the model and adds the image title/description
        var post = new ImagesModel
        {
            Title = title,
            Description = description,
            FilePath = "/uploads/" + fileName,
            UserId = CurrentUserId().Value
        };

        _database.Images.Add(post);
        await _database.SaveChangesAsync();

        return RedirectToAction("Gallery");
    }

    [HttpGet]
    public IActionResult Gallery()
    {
        if (CurrentUserId() == null)
            return RedirectToAction("Login", "Account");

        var images = _database.Images
            .Include(i => i.User)
            .ToList();

        return View(images);
    }

    //The AJAX call
    [HttpGet]
    public IActionResult GetCount()
    {
        if (CurrentUserId() == null) return Json(new { success = false });
        var count = _database.Images.Count();
        return Json(new { success = true, count });
    }
}