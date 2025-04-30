using Microsoft.EntityFrameworkCore;
using impoTrack.Models; 

namespace impoTrack.Data
{
    public class impoTrackContext : DbContext
    {
        public impoTrackContext(DbContextOptions<impoTrackContext> options) : base(options)
        {
        }

        // Define tus DbSet<T> aquí, uno por cada modelo que quieras mapear a la base de datos
        public DbSet<Entrega> Entregas { get; set; }
        public DbSet<Informe> Informes { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ProblemaEntrega> ProblemasEntrega { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Repartidor> Repartidores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }        
    }
}