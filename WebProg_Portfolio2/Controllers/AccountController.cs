using System.Text.RegularExpressions;
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
            // Hvis regex eller required fejler, returneres view med fejlbeskeder
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

    
    /*---------------------------
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

   
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string username, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            ModelState.AddModelError("", "Alle felter er ikke udfyldt endnu");
            return View();
        }

        //EMAIL check regex
        var emailRegex = new Regex(@"^[a-zA-Z._-]+@[a-zA-Z._-]+\.[a-z]{2,10}$");
        if (!emailRegex.IsMatch(email))
        {
            ModelState.AddModelError("", "E-mail adresse ej valid");
            return View();
        }

        var hash = Convert.ToBase64String(SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(password)));

        var user = new UsersModel
        {
            Username = username, Email = email, HashedPassword = hash
        };

        _database.Users.Add(user);
        await _database.SaveChangesAsync();

        HttpContext.Session.SetInt32("UserId", user.Id);
        return RedirectToAction("Upload", "Images");

        ---------------------------*/
        
        /*
        //PASSWORD check regex
        if (!Regex.IsMatch(password, "[a-z]"))
        {
            ModelState.AddModelError("", "You must include a lower-case letter");
            return View();
        }

        if (!Regex.IsMatch(password, "[A-Z]"))
        {
            ModelState.AddModelError("", "You must include an upper-case letter");
            return View();
        }

        if (!Regex.IsMatch(password, "[0-9]"))
        {
            ModelState.AddModelError("", "You must include a number");
            return View();
        }

        if (!Regex.IsMatch(password, "[:;!\"#?¤%&/()=*,.^><_-]"))
        {
            ModelState.AddModelError("", "You must include a special character");
            return View();
        }

        if (password.Length < 8)
        {
            ModelState.AddModelError("", "Your password must be at least 8 characters");
            return View();
        }

        return View();
    } */

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