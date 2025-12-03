using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using WebProg_Portfolio2.Models;
using System.Security.Cryptography;

namespace WebProg_Portfolio2.Controllers;

public class ImagesController : Controller
{
    private int? CurrentUserId() => HttpContext.Session.GetInt32("UserId");


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

        // Simple sikkerhed: extension check
        var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var ext = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
        if (!allowed.Contains(ext))
        {
            ModelState.AddModelError("", "Ugyldig filtype");
            return View();
        }

        // Gem fil
        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(uploads);
        var fileName = Guid.NewGuid().ToString() + ext;
        var filePath = Path.Combine(uploads, fileName);
        using (var stream = System.IO.File.Create(filePath))
        {
            await imageFile.CopyToAsync(stream);
        }
        return View();
    }
}
/*
var post = new ImagePost {
    Title = title,
    Description = description,
    FilePath = "/uploads/" + fileName,
    UserId = CurrentUserId().Value
};
_database.ImagePosts.Add(post);
await _database.SaveChangesAsync();
return RedirectToAction("Gallery");
}*/

/*
[HttpGet]
public IActionResult GetCount()
{
    if (CurrentUserId() == null) return Json(new { success = false });
    var count = _database.ImagePosts.Count();
    return Json(new { success = true, count });
}
*/