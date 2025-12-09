using Microsoft.AspNetCore.Mvc;
using WebProg_Portfolio2.Models;

namespace WebProg_Portfolio2.Controllers;

public class UsersController : Controller
{
    private readonly AppDbContext _database;
    public UsersController(AppDbContext database)
    {
        _database = database;
    }
    
    [HttpGet]
    public IActionResult List()
    {
        var users = _database.Users.Select(u => new {u.Id, u.Username, u.Email}).ToList();
        return View(users);
    }
}