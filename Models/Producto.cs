using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace impoTrack.Models;

public class Producto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductoID { get; set; }

    [Required]
    [MaxLength(100)]
    public string NombreProducto { get; set; }

    [Required]
    public string Descripcion { get; set; }

    [Required]
    [Column(TypeName = "decimal(10, 2)")] // Configura el tipo de dato decimal
    public decimal Precio { get; set; }

    public List<Pedido> Pedidos { get; set; }
}