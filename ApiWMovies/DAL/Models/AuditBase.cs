using System.ComponentModel.DataAnnotations;

namespace ApiWMovies.DAL.Models;

public class AuditBase
{
    [Key]
    public int id { get; set; }

    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}

