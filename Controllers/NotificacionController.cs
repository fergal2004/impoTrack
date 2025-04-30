using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using impoTrack.Data;
using impoTrack.Models;

namespace impoTrack.Controllers
{
    public class NotificacionController : Controller
    {
        private readonly impoTrackContext _context;

        public NotificacionController(impoTrackContext context)
        {
            _context = context;
        }

        // GET: Notificacion
        public async Task<IActionResult> Index()
        {
            var impoTrackContext = _context.Notificaciones.Include(n => n.Pedido).Include(n => n.Usuario);
            return View(await impoTrackContext.ToListAsync());
        }

        // GET: Notificacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificacion = await _context.Notificaciones
                .Include(n => n.Pedido)
                .Include(n => n.Usuario)
                .FirstOrDefaultAsync(m => m.NotificacionID == id);
            if (notificacion == null)
            {
                return NotFound();
            }

            return View(notificacion);
        }

        // GET: Notificacion/Create
        public IActionResult Create()
        {
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "PedidoID", "DireccionEntrega");
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Email");
            return View();
        }

        // POST: Notificacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotificacionID,UsuarioID,PedidoID,TipoNotificacion,Mensaje,FechaEnvio")] Notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "PedidoID", "DireccionEntrega", notificacion.PedidoID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Email", notificacion.UsuarioID);
            return View(notificacion);
        }

        // GET: Notificacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null)
            {
                return NotFound();
            }
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "PedidoID", "DireccionEntrega", notificacion.PedidoID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Email", notificacion.UsuarioID);
            return View(notificacion);
        }

        // POST: Notificacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotificacionID,UsuarioID,PedidoID,TipoNotificacion,Mensaje,FechaEnvio")] Notificacion notificacion)
        {
            if (id != notificacion.NotificacionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificacionExists(notificacion.NotificacionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "PedidoID", "DireccionEntrega", notificacion.PedidoID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Email", notificacion.UsuarioID);
            return View(notificacion);
        }

        // GET: Notificacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificacion = await _context.Notificaciones
                .Include(n => n.Pedido)
                .Include(n => n.Usuario)
                .FirstOrDefaultAsync(m => m.NotificacionID == id);
            if (notificacion == null)
            {
                return NotFound();
            }

            return View(notificacion);
        }

        // POST: Notificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion != null)
            {
                _context.Notificaciones.Remove(notificacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificacionExists(int id)
        {
            return _context.Notificaciones.Any(e => e.NotificacionID == id);
        }
    }
}
