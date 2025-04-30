using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace impoTrack.Models;

public class Notificacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NotificacionID { get; set; }

    [Required]
    public int UsuarioID { get; set; }
    [ForeignKey("UsuarioID")]
    public Usuario Usuario { get; set; }

    public int? PedidoID { get; set; }
    [ForeignKey("PedidoID")]
    public Pedido Pedido { get; set; }

    [Required]
    [MaxLength(50)]
    public string TipoNotificacion { get; set; }

    [Required]
    public string Mensaje { get; set; }

    [Required]
    public DateTime FechaEnvio { get; set; }
}