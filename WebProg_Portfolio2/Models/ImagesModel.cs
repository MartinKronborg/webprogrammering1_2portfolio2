using System.ComponentModel.DataAnnotations;

namespace WebProg_Portfolio2.Models;

public class ImagesModel
{
    public int Id { get; set; } //the PK
    
    [Required(ErrorMessage = "Titel er påkrævet")]
    [RegularExpression(@"^[a-zA-Z0-9 æøåÆØÅ]{3,50}$", 
        ErrorMessage = "Titel må kun indeholde bogstaver, tal og mellemrum (3-50 tegn)")]
    
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Beskrivelse er påkrævet")]
    [RegularExpression(@"^[a-zA-Z0-9 .,æøåÆØÅ]{5,200}$",
        ErrorMessage = "Beskrivelse må kun indeholde bogstaver, tal og ., (5-200 tegn)")]
    
    public string Description { get; set; }
    public string FilePath { get; set; }
    public int UserId { get; set; }
    
    public UsersModel? User { get; set; }
}