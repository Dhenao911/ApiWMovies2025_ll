using System.ComponentModel.DataAnnotations;

namespace ApiWMovies.DAL.Dtos
{
    public class MovieUpdateCreateDto
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Movie Name")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Duration { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(10, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Clasification { get; set; }
    }
}