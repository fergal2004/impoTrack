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
    public class EntregaController : Controller
    {
        private readonly impoTrackContext _context;

        public EntregaController(impoTrackContext context)
        {
            _context = context;
        }

        // GET: Entrega
        public async Task<IActionResult> Index()
        {
            var impoTrackContext = _context.Entregas.Include(e => e.Pedido).Include(e => e.Repartidor);
            return View(await impoTrackContext.ToListAsync());
        }

        // GET: Entrega/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrega = await _context.Entregas
                .Include(e => e.Pedido)
                .Include(e => e.Repartidor)
                .FirstOrDefaultAsync(m => m.EntregaID == id);
            if (entrega == null)
            {
                return NotFound();
            }

            return View(entrega);
        }

        // GET: Entrega/Create
        public IActionResult Create()
        {
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "PedidoID", "DireccionEntrega");
            ViewData["RepartidorID"] = new SelectList(_context.Repartidores, "RepartidorID", "Apellido");
            return View();
        }

        // POST: Entrega/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntregaID,PedidoID,RepartidorID,ZonaEntrega,RutaEntrega,FechaAsignacion,FechaEntregaReal")] Entrega entrega)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entrega);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "PedidoID", "DireccionEntrega", entrega.PedidoID);
            ViewData["RepartidorID"] = new SelectList(_context.Repartidores, "RepartidorID", "Apellido", entrega.RepartidorID);
            return View(entrega);
        }

        // GET: Entrega/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrega = await _context.Entregas.FindAsync(id);
            if (entrega == null)
            {
                return NotFound();
            }
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "PedidoID", "DireccionEntrega", entrega.PedidoID);
            ViewData["RepartidorID"] = new SelectList(_context.Repartidores, "RepartidorID", "Apellido", entrega.RepartidorID);
            return View(entrega);
        }

        // POST: Entrega/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntregaID,PedidoID,RepartidorID,ZonaEntrega,RutaEntrega,FechaAsignacion,FechaEntregaReal")] Entrega entrega)
        {
            if (id != entrega.EntregaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entrega);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntregaExists(entrega.EntregaID))
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
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "PedidoID", "DireccionEntrega", entrega.PedidoID);
            ViewData["RepartidorID"] = new SelectList(_context.Repartidores, "RepartidorID", "Apellido", entrega.RepartidorID);
            return View(entrega);
        }

        // GET: Entrega/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrega = await _context.Entregas
                .Include(e => e.Pedido)
                .Include(e => e.Repartidor)
                .FirstOrDefaultAsync(m => m.EntregaID == id);
            if (entrega == null)
            {
                return NotFound();
            }

            return View(entrega);
        }

        // POST: Entrega/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entrega = await _context.Entregas.FindAsync(id);
            if (entrega != null)
            {
                _context.Entregas.Remove(entrega);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntregaExists(int id)
        {
            return _context.Entregas.Any(e => e.EntregaID == id);
        }

        //  ---------------------------------------------------------------------
        //  Módulo "Gestionar Entregas" Funcionalidad
        //  ---------------------------------------------------------------------

        //  GET: Entrega/AsignarRepartidor/5 (5 is PedidoID)
        public IActionResult AsignarRepartidor(int pedidoId)
        {
            ViewBag.PedidoId = pedidoId;
            ViewData["RepartidorID"] = new SelectList(_context.Repartidores.Where(r => r.Estado == "Activo"), "RepartidorID", "Nombre"); //  Only active
            return View();
        }

        //  POST: Entrega/AsignarRepartidor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarRepartidor(int pedidoId, int repartidorId, string zonaEntrega, string rutaEntrega, DateTime fechaAsignacion)
        {
            var entrega = new Entrega
            {
                PedidoID = pedidoId,
                RepartidorID = repartidorId,
                ZonaEntrega = zonaEntrega,
                RutaEntrega = rutaEntrega,
                FechaAsignacion = fechaAsignacion
            };

            _context.Add(entrega);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));  //  Or another appropriate action
        }

        //  GET: Entrega/RepartidoresDisponibles
        public async Task<IActionResult> RepartidoresDisponibles()
        {
            var repartidoresDisponibles = await _context.Repartidores.Where(r => r.Estado == "Activo").ToListAsync();
            return View(repartidoresDisponibles); //  Use a view to display available drivers
        }
    }
}