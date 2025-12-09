using Microsoft.AspNetCore.Mvc;
using WebProg_Portfolio2.Models;
using System.Security.Cryptography;

namespace WebProg_Portfolio2.Controllers;

public class AccountController : Controller

{
    private readonly AppDbContext _database;

    public AccountController(AppDbContext database)
    {
        _database = database;
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UsersModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var hash = Convert.ToBase64String(SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(model.HashedPassword)));

        var user = new UsersModel
        {
            Username = model.Username,
            Email = model.Email,
            HashedPassword = hash
        };

        _database.Users.Add(user);
        await _database.SaveChangesAsync();

        HttpContext.Session.SetInt32("UserId", user.Id);
        return RedirectToAction("Upload", "Images");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string emailOrUsername, string password)
    {
        var hash = Convert.ToBase64String(SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(password)));
        var user = _database.Users.FirstOrDefault(u =>
            (u.Email == emailOrUsername || u.Username == emailOrUsername) && u.HashedPassword == hash);
       
        if (user == null)
        {
            ModelState.AddModelError("", "Ukendt bruger");
            return View();
        }
        HttpContext.Session.SetInt32("UserId", user.Id);
        return RedirectToAction("Upload", "Images");

    }
}