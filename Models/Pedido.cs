using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace impoTrack.Models;

public class Pedido
{
    [Key] // Clave Primaria
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Autoincremental
    public int PedidoID { get; set; }

    [Required]
    [Display(Name = "Fecha de Pedido")]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "El formato de fecha debe ser AAAA-MM-DD.")]
    public String FechaPedido { get; set; }

    [Required]
    [MaxLength(50)] // Limita la longitud de la cadena
    public string EstadoPedido { get; set; }

    [Required]
    [MaxLength(255)]
    public string DireccionEntrega { get; set; }

    [MaxLength(100)]
    public string ClienteNombre { get; set; } // O int ClienteID (FK)

    public int RepartidorID { get; set; } // Nullable FK
    [ForeignKey("RepartidorID")] // Define la relación con Repartidor
    public Repartidor? Repartidor { get; set; }

    [Required]
    public int ProductoID { get; set; }
    [ForeignKey("ProductoID")] // Define la relación con Producto
    public Producto? Producto { get; set; }

    [Required]
    [Display(Name = "Fecha de Entrega Estimada")]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "El formato de fecha debe ser AAAA-MM-DD.")]
    public string FechaEntregaEstimada { get; set; }

    // Propiedades de navegación (EF Core)
    public List<Entrega>? Entregas { get; set; }
    public List<ProblemaEntrega>? ProblemasEntrega { get; set; }
}