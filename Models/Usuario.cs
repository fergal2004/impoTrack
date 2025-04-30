using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace impoTrack.Models;

public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UsuarioID { get; set; }

    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; }

    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [MaxLength(255)]
    public string Password { get; set; } // ¡ENCRIPTADA!

    [Required]
    [MaxLength(20)]
    public string Rol { get; set; }

    public List<Notificacion> Notificaciones { get; set; } 
}