using System.ComponentModel.DataAnnotations;

namespace ApiWMovies.DAL.Dtos
{
    public class MovieUpdateDto
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Movie Name")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Name { get; set; }
    }
}