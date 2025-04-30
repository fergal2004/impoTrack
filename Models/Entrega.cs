using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace impoTrack.Models;

public class Entrega
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EntregaID { get; set; }

    [Required]
    public int PedidoID { get; set; }
    [ForeignKey("PedidoID")]
    public Pedido Pedido { get; set; }

    public int? RepartidorID { get; set; }
    [ForeignKey("RepartidorID")]
    public Repartidor Repartidor { get; set; }

    [Required]
    [MaxLength(50)]
    public string ZonaEntrega { get; set; }

    [Required]
    public string RutaEntrega { get; set; }

    [Required]
    public DateTime FechaAsignacion { get; set; }

    public DateTime? FechaEntregaReal { get; set; } 
}