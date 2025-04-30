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
    public class ProblemaEntregaController : Controller
    {
        private readonly impoTrackContext _context;

        public ProblemaEntregaController(impoTrackContext context)
        {
            _context = context;
        }

        // GET: ProblemaEntrega
        public async Task<IActionResult> Index()
        {
            var impoTrackContext = _context.ProblemasEntrega.Include(p => p.Pedido);
            return View(await impoTrackContext.ToListAsync());
        }

        // GET: ProblemaEntrega/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemaEntrega = await _context.ProblemasEntrega
                .Include(p => p.Pedido)
                .FirstOrDefaultAsync(m => m.ProblemaID == id);
            if (problemaEntrega == null)
            {
                return NotFound();
            }

            return View(problemaEntrega);
        }

        // GET: ProblemaEntrega/Create
        public IActionResult Create()
        {
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "PedidoID", "DireccionEntrega");
            return View();
        }

        // POST: ProblemaEntrega/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProblemaID,PedidoID,DescripcionProblema,FechaReporte,EstadoProblema,Responsable,Solucion")] ProblemaEntrega problemaEntrega)
        {
            if (ModelState.IsValid)
            {
                _context.Add(problemaEntrega);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "PedidoID", "DireccionEntrega", problemaEntrega.PedidoID);
            return View(problemaEntrega);
        }

        // GET: ProblemaEntrega/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemaEntrega = await _context.ProblemasEntrega.FindAsync(id);
            if (problemaEntrega == null)
            {
                return NotFound();
            }
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "PedidoID", "DireccionEntrega", problemaEntrega.PedidoID);
            return View(problemaEntrega);
        }

        // POST: ProblemaEntrega/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProblemaID,PedidoID,DescripcionProblema,FechaReporte,EstadoProblema,Responsable,Solucion")] ProblemaEntrega problemaEntrega)
        {
            if (id != problemaEntrega.ProblemaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problemaEntrega);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemaEntregaExists(problemaEntrega.ProblemaID))
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
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "PedidoID", "DireccionEntrega", problemaEntrega.PedidoID);
            return View(problemaEntrega);
        }

        // GET: ProblemaEntrega/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemaEntrega = await _context.ProblemasEntrega
                .Include(p => p.Pedido)
                .FirstOrDefaultAsync(m => m.ProblemaID == id);
            if (problemaEntrega == null)
            {
                return NotFound();
            }

            return View(problemaEntrega);
        }

        // POST: ProblemaEntrega/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problemaEntrega = await _context.ProblemasEntrega.FindAsync(id);
            if (problemaEntrega != null)
            {
                _context.ProblemasEntrega.Remove(problemaEntrega);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemaEntregaExists(int id)
        {
            return _context.ProblemasEntrega.Any(e => e.ProblemaID == id);
        }
    }
}
