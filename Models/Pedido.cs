using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace impoTrack.Models;

public class Pedido
{
    [Key] // Clave Primaria
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Autoincremental
    public int PedidoID { get; set; }

    [Required] // No puede ser nulo en la base de datos
    public DateTime FechaPedido { get; set; }

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
    public DateTime FechaEntregaEstimada { get; set; }

    // Propiedades de navegación (EF Core)
    public List<Entrega>? Entregas { get; set; }
    public List<ProblemaEntrega>? ProblemasEntrega { get; set; }
}