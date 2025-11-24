using System.ComponentModel.DataAnnotations;

namespace ApiWMovies.DAL.Models;

public class Category : AuditBase
{
    [Display(Name = "Category Name")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    [MaxLength(100, ErrorMessage = "El campo {0} tiene maximo {1} caracteres")]
    public string Name { get; set; }
}