using System.ComponentModel.DataAnnotations;

namespace ApiWMovies.DAL.Dtos
{
    public class CategoryUpdateCreateDto
    {

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} tiene maximo {1} caracteres")]
        public string Name { get; set; }
    }
}
