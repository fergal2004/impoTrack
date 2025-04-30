using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace impoTrack.Models;

public class Repartidor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RepartidorID { get; set; }

    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; }

    [Required]
    [MaxLength(50)]
    public string Apellido { get; set; }

    [Required]
    [MaxLength(20)]
    public string Telefono { get; set; }

    [Required]
    [MaxLength(20)]
    public string Estado { get; set; }

    [MaxLength(100)]
    public string UbicacionActual { get; set; }

    public List<Pedido>? Pedidos { get; set; }
    public List<Entrega>? Entregas { get; set; }
}