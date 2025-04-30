using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace impoTrack.Models;

public class Informe
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InformeID { get; set; }

    [Required]
    [MaxLength(50)]
    public string TipoInforme { get; set; }

    [Required]
    public DateTime FechaGeneracion { get; set; }

    [Required]
    public string DatosInforme { get; set; }
}