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
    public class PedidoController : Controller
    {
        private readonly impoTrackContext _context;

        public PedidoController(impoTrackContext context)
        {
            _context = context;
        }

        // GET: Pedido
        public async Task<IActionResult> Index()
        {
            var impoTrackContext = _context.Pedidos.Include(p => p.Producto).Include(p => p.Repartidor);
            return View(await impoTrackContext.ToListAsync());
        }

        // GET: Pedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Producto)
                .Include(p => p.Repartidor)
                .FirstOrDefaultAsync(m => m.PedidoID == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedido/Create
        // GET: Pedido/Create
        public IActionResult Create()
        {
            ViewBag.RepartidorID = new SelectList(_context.Repartidores.Select(r => new { Value = r.RepartidorID.ToString(), Text = r.Nombre }), "Value", "Text");
            ViewBag.ProductoID = new SelectList(_context.Productos.Select(p => new { Value = p.ProductoID.ToString(), Text = p.NombreProducto }), "Value", "Text");
            return View();
        }

        // POST: Pedido/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoID,FechaPedido,EstadoPedido,DireccionEntrega,ClienteNombre,RepartidorID,ProductoID,FechaEntregaEstimada")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.RepartidorID = new SelectList(_context.Repartidores, "RepartidorID", "NombreCompleto", pedido.RepartidorID);
            ViewBag.ProductoID = new SelectList(_context.Productos, "ProductoID", "NombreProducto", pedido.ProductoID);
            return View(pedido);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewBag.ProductoID = new SelectList(_context.Productos, "ProductoID", "NombreProducto", pedido.ProductoID); // Cambiado a NombreProducto
            ViewBag.RepartidorID = new SelectList(_context.Repartidores, "RepartidorID", "NombreCompleto", pedido.RepartidorID); // Cambiado a NombreCompleto
            return View(pedido);
        }

        // POST: Pedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoID,FechaPedido,EstadoPedido,DireccionEntrega,ClienteNombre,RepartidorID,ProductoID,FechaEntregaEstimada")] Pedido pedido)
        {
            if (id != pedido.PedidoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.PedidoID))
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
            ViewBag.ProductoID = new SelectList(_context.Productos, "ProductoID", "NombreProducto", pedido.ProductoID); // Cambiado a NombreProducto
            ViewBag.RepartidorID = new SelectList(_context.Repartidores, "RepartidorID", "NombreCompleto", pedido.RepartidorID); // Cambiado a NombreCompleto
            return View(pedido);
        }

        // GET: Pedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Producto)
                .Include(p => p.Repartidor)
                .FirstOrDefaultAsync(m => m.PedidoID == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.PedidoID == id);
        }
    }
}