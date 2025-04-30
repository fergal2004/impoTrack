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
    public class RepartidorController : Controller
    {
        private readonly impoTrackContext _context;

        public RepartidorController(impoTrackContext context)
        {
            _context = context;
        }

        // GET: Repartidor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Repartidores.ToListAsync());
        }

        // GET: Repartidor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repartidor = await _context.Repartidores
                .FirstOrDefaultAsync(m => m.RepartidorID == id);
            if (repartidor == null)
            {
                return NotFound();
            }

            return View(repartidor);
        }

        // GET: Repartidor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Repartidor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RepartidorID,Nombre,Apellido,Telefono,Estado,UbicacionActual")] Repartidor repartidor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(repartidor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(repartidor);
        }

        // GET: Repartidor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repartidor = await _context.Repartidores.FindAsync(id);
            if (repartidor == null)
            {
                return NotFound();
            }
            return View(repartidor);
        }

        // POST: Repartidor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RepartidorID,Nombre,Apellido,Telefono,Estado,UbicacionActual")] Repartidor repartidor)
        {
            if (id != repartidor.RepartidorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repartidor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepartidorExists(repartidor.RepartidorID))
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
            return View(repartidor);
        }

        // GET: Repartidor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repartidor = await _context.Repartidores
                .FirstOrDefaultAsync(m => m.RepartidorID == id);
            if (repartidor == null)
            {
                return NotFound();
            }

            return View(repartidor);
        }

        // POST: Repartidor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repartidor = await _context.Repartidores.FindAsync(id);
            if (repartidor != null)
            {
                _context.Repartidores.Remove(repartidor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepartidorExists(int id)
        {
            return _context.Repartidores.Any(e => e.RepartidorID == id);
        }
    }
}
