namespace WebProg_Portfolio2.Models;

public class UsersModel
{
    public int Id { get; set; } //the PK
    public string Username { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }


    public List<ImagesModel> Images { get; set; } = new();
}