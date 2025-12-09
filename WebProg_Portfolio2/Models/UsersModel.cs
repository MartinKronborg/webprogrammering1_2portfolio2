using System.ComponentModel.DataAnnotations;

namespace WebProg_Portfolio2.Models;

public class UsersModel
{
    public int Id { get; set; } //the primary key
    
    [Required(ErrorMessage = "Brugernavn påkrævet")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Brugernavn må kun indeholde bogstaver og tal.")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Email påkrævet")]
    [EmailAddress(ErrorMessage = "Indtast en gyldig email.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Adgangskode påkrævet")]
    public string HashedPassword { get; set; }
    
    public List<ImagesModel> Images { get; set; } = new();
}