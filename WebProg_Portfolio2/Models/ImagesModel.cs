namespace WebProg_Portfolio2.Models;

public class ImagesModel
{
    public int Id { get; set; } //the PK
    public string Title { get; set; }
    public string Description { get; set; }
    public string FilePath { get; set; }
    public int UserId { get; set; }
    public UsersModel User { get; set; }
    
   
    
}