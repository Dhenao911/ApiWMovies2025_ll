using System.ComponentModel.DataAnnotations;

namespace ApiWMovies.DAL.Models;

public class Movie : AuditBase
{
    [Required]
    [Display(Name = "Movie Name")]
    public string Name { get; set; }

    [Required]
    public int Duration { get; set; }

   
    public string? Description { get; set; }

    [Required]
    public string Clasification { get; set; }


}