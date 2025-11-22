using System.ComponentModel.DataAnnotations;

namespace ApiWMovies.DAL.Models;

public class Category : AuditBase
{
    [Required]
    [Display(Name = "Category Name")]
    public string Name { get; set; }
}