using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace impoTrack.Models;

public class ProblemaEntrega
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProblemaID { get; set; }

    [Required]
    public int PedidoID { get; set; }
    [ForeignKey("PedidoID")]
    public Pedido Pedido { get; set; }

    [Required]
    public string DescripcionProblema { get; set; }

    [Required]
    public DateTime FechaReporte { get; set; }

    [Required]
    [MaxLength(20)]
    public string EstadoProblema { get; set; }

    [MaxLength(50)]
    public string Responsable { get; set; }

    public string Solucion { get; set; }
}